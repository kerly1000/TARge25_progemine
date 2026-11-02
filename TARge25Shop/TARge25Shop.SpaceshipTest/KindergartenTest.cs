
using TARge25Shop.Core.Domain;
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

        [Fact] //4 testime, et peale loomist tagastab sama id-ga lasteaia
        public async Task Should_GetKinergartenById_WhenGuidIsEqual()
        {
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)createdKindergarten.Id);
                        
            Assert.NotNull(result);
            Assert.Equal(createdKindergarten.Id, result.Id);
            Assert.Equal(createdKindergarten.KindergartenName, result.KindergartenName);
        }

        [Fact] //5 testime, et kustutatakse lasteaed, mille id-ga kustutamist küsime

        public async Task Should_DeleteKindergartenById_WhenReturnedResultIsEqual()
        {
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            var deleteResult = await Svc<IKindergartenServices>().Delete((Guid)createdKindergarten.Id);

            Assert.NotNull(deleteResult);
            Assert.Equal(createdKindergarten.Id, deleteResult.Id);
        }

        [Fact] //6 testime, kas update korral muudatused salvestati samale id-le

        public async Task Should_UpdateKindergarten_WhenResultIsReturned()
        {
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            dto.Id = createdKindergarten.Id;
            dto.GroupName = "Updated Groupname";
            dto.KindergartenName = "Updated Kindergarten";
            dto.TeacherName = "Updated Teacher";
            dto.ChildrenCount = 15;
                        
            var updatedKindergarten = await Svc<IKindergartenServices>().Update(dto);
                        
            Assert.NotNull(updatedKindergarten);
            Assert.Equal("Updated Groupname", updatedKindergarten.GroupName);
            Assert.Equal("Updated Kindergarten", updatedKindergarten.KindergartenName);
            Assert.Equal("Updated Teacher", updatedKindergarten.TeacherName);
            Assert.Equal(15, updatedKindergarten.ChildrenCount);
            Assert.Equal(createdKindergarten.Id, updatedKindergarten.Id);
        }

        [Fact] //7 testime, et õpetaja nimi ei oleks lühem kui 3 tähte
        public async Task ShouldNot_UpdateKindergarten_WhenTeacherNameIsTooShort()
        {            
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            dto.Id = createdKindergarten.Id;
            dto.TeacherName = "AB";
                        
            var result = await Svc<IKindergartenServices>().Update(dto);
                        
            Assert.NotNull(createdKindergarten);
            Assert.Null(result);
        }

        [Fact] //8 testime, et kirjet ei looda, kui laste arv on negatiivne
        public async Task ShouldNot_CreateKindergarten_WhenChildrenCountValueIsNegative()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.ChildrenCount = -5;
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                 
            Assert.Null(result);
            
        }

        [Fact] //9 testime, et kirjet ei looda, kui laste arv on liiga suur
        public async Task ShouldNot_CreateKindergarten_WhenChildrenCountIsTooHigh()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.ChildrenCount = 45;
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.Null(result);
        }

        [Fact] //10
        public async Task ShouldNot_GetKindergartenById_WhenIdIsDeleted()
        {            
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);
                        
            await Svc<IKindergartenServices>().Delete((Guid)createdKindergarten.Id);
            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)createdKindergarten.Id);
                        
            Assert.Null(result);
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
