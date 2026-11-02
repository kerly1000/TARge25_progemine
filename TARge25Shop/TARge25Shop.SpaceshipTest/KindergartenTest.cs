
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop.SpaceshipTest
{
    public class KindergartenTest : TestBase
    {
        //1-11 spaceshipi järgi testid

        [Fact] //1 testime create'i
        public async Task Should_AddKindergarten_WhenResultIsReturned()
        {
            // Ülesseade
            KindergartenDto dto = new KindergartenDto()
            {
                GroupName = "Test Groupname",
                ChildrenCount = 20,
                KindergartenName = "Test Kindergarten",
                TeacherName = "Test Teacher",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.NotNull(result);
        }

        [Fact] //2 testime, et ei tagastataks objekti, kui id != id
        public async Task ShouldNot_GetKindergartenByID_WhenGuidIsNotEqual()
        {
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);
            Guid wrongGuid = Guid.NewGuid();

            var result = await Svc<IKindergartenServices>().DetailAsync(wrongGuid);

            Assert.NotNull(createdKindergarten);
            Assert.Null(result);
        }

        [Fact] //3 testime, et ei kustuta lasteaeda, mille id != id
        public async Task ShouldNot_DeleteKindergartenByID_WhenReturnedResultIsNotEqual()
        {
            var dto = MockKindergartenData();
            var createdKindergarten1 = await Svc<IKindergartenServices>().Create(dto);
            var createdKindergarten2 = await Svc<IKindergartenServices>().Create(dto);
            var deleteResult = await Svc<IKindergartenServices>().Delete((Guid)createdKindergarten2.Id);
            var kindergartenindb = await Svc<IKindergartenServices>().DetailAsync((Guid)createdKindergarten1.Id);

            //kontroll
            Assert.NotNull(deleteResult);
            Assert.NotNull(kindergartenindb);
            Assert.NotEqual(deleteResult.Id, createdKindergarten1.Id);
            Assert.Equal(createdKindergarten1, kindergartenindb);
        }

        [Fact] //4 

        public async Task Should_GetKinergartenById_WhenGuidIsEqual()
        {
            KindergartenDto dto = MockKindergartenData();

        }


        //abimeetodid
        private KindergartenDto MockKindergartenData(bool isOneOrTwo = false)
        {
            if (isOneOrTwo == false)
            {
                return new KindergartenDto
                {
                    GroupName = "Test Groupname",
                    ChildrenCount = 20,
                    KindergartenName = "Test Kindergarten",
                    TeacherName = "Test Teacher",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            }
            else
            {
                return new KindergartenDto
                {
                    GroupName = "Groupname Test",
                    ChildrenCount = 20,
                    KindergartenName = "Kindergarten Test",
                    TeacherName = "Teacher Test",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            }
        }
    }
}
