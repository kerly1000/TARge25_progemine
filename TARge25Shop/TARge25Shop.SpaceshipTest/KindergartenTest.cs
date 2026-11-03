
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

        [Fact] //7 testime, et õpetaja nimi ei oleks update korral lühem kui 3 tähte
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

        [Fact] //10 testime, et ei tahastaks kustutatud lasteaeda
        public async Task ShouldNot_GetKindergartenById_WhenIdIsDeleted()
        {            
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);
                        
            await Svc<IKindergartenServices>().Delete((Guid)createdKindergarten.Id);
            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)createdKindergarten.Id);
                        
            Assert.Null(result);
        }

        [Fact] //11 testime, et update korral ei muutu created at andmed
        public async Task Should_KeepCreatedAtAndChangeUpdatedAt_WhenKindergartenIsUpdated()
        {
            KindergartenDto dto = MockKindergartenData();
            var createdKindergarten = await Svc<IKindergartenServices>().Create(dto);

            DateTime originalCreatedAt = createdKindergarten.CreatedAt;

            dto.Id = createdKindergarten.Id;
            dto.GroupName = "Updated Group";
                        
            var updatedKindergarten = await Svc<IKindergartenServices>().Update(dto);
                        
            Assert.NotNull(updatedKindergarten);
            Assert.Equal(originalCreatedAt, updatedKindergarten.CreatedAt);
            Assert.True(updatedKindergarten.UpdatedAt >= createdKindergarten.UpdatedAt);
        }

        [Fact] //12 testime, kas kõik andmed salvestatakse kirje loomisel korrektselt
        public async Task Should_SaveAllKindergartenData_WhenCreated()
        {            
            KindergartenDto dto = MockKindergartenData();
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.NotNull(result);
            Assert.Equal(dto.GroupName, result.GroupName);
            Assert.Equal(dto.ChildrenCount, result.ChildrenCount);
            Assert.Equal(dto.KindergartenName, result.KindergartenName);
            Assert.Equal(dto.TeacherName, result.TeacherName);
        }

        [Fact] //13 testime, et ei loodaks uut kirjet, kui rühma nime väli on tühi
        public async Task ShouldNot_CreateKindergarten_WhenGroupNameIsEmpty()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.GroupName = "";
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.Null(result);
        }

        [Fact] //14 testime, et ei loodaks uut kirjet, kui lasteaia nime väli on tühi
        public async Task ShouldNot_CreateKindergarten_WhenKindergartenNameIsEmpty()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.KindergartenName = "";
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.Null(result);
        }

        [Fact] //15 testime, et ei loodaks uut kirjet, kui õpetaja nime väli on tühi
        public async Task ShouldNot_CreateKindergarten_WhenTeacherNameIsEmpty()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.TeacherName = "";
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.Null(result);
        }

        [Fact] //16 testime, et piirväärtuse maksimum võetakse vastu ja luuakse kirje
        public async Task Should_CreateKindergarten_WhenChildrenCountIsThirty()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.ChildrenCount = 30;
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.NotNull(result);
            Assert.Equal(30, result.ChildrenCount);
        }

        [Fact] //17 testime, et miinimumväärtus 0 võetakse vastu ja luuakse kirje
        public async Task Should_CreateKindergarten_WhenChildrenCountIsZero()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.ChildrenCount = 0;
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.NotNull(result);
            Assert.Equal(0, result.ChildrenCount);
        }

        [Fact] //18 testime, et kirjet ei looda, kui nimi sisaldab ainult tühikuid
        public async Task ShouldNot_CreateKindergarten_WhenGroupNameContainsOnlySpaces()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.GroupName = "   ";
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
            Assert.Null(result);
        }

        [Fact] //19 testime, et uue lasteaeda ei looda, kui õpetaja nimi on liiga lühike
        public async Task ShouldNot_CreateKindergarten_WhenTeacherNameIsTooShort()
        {            
            KindergartenDto dto = MockKindergartenData();
            dto.TeacherName = "AB";
                        
            var result = await Svc<IKindergartenServices>().Create(dto);
                        
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
