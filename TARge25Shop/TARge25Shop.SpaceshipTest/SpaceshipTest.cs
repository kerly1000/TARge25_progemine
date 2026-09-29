using System;
using System.Collections.Generic;
using System.Text;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {

        [Fact] //Fact tähistab ära ühe testi xUnit raamistikus
        //  1- kirjeldatakse, kas test on tavaline või nagatiivne
        //  2- kirjeldatakse, mida üritatakse testialuse objektiga teha
        //  3- mis tingimustel tulemust kontrollitakse peale tegevust
        //
        // Selles testis kontrollitakse- (2) kosmoselaeva lisamisel
        //(1) ei tohiks (3) saadud tulemus olla tühi.
        public async Task ShouldNot_AddEmptySpaceship_WhenResultIsReturned()
        {
            //Ülesseade
            SpaceshipDto dto = new SpaceshipDto()
            {
                Name = "X Space",
                ShipType = "rakett",
                Crew = 2,
                EnginePower = 12,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            //tegutsemine
            var result = await Svc<ISpaceshipServices>().Create(dto);

            //kontroll
            Assert.NotNull(result);
        }

        //Kontrollitakse, et: Spaceshipi päring andmebaasist ei tohi tagastada objekti kui Id-d ei ole võrdsed
        [Fact]

        public async Task ShouldNot_GetSpaceshipById_WhenIdNotEqual()
        {
            // ülesseade
            Guid wrongGuid = Guid.NewGuid();
            Guid goodGuid = Guid.Parse("3dd49e7f-7721-4673-9b6a-db11ecb36919");

            //tegevus
            await Svc<ISpaceshipServices>().DetailAsync(goodGuid);

            Assert.NotEqual(wrongGuid, goodGuid);

        }


        //Seleta kodus lahti, nagu eelnevate testide laused eesti keelde. 
        //selles testis me kontrollime, et kosmoselaeva databasest peaks tagastama spaceshipi, kui id == id
        [Fact]

        public async Task Should_GetSpaceshipById_WhenGuidIsequal()
        {
            // ülesseade
            Guid databaseGuid = Guid.Parse("3dd49e7f-7721-4673-9b6a-db11ecb36919");
            Guid seekGuid = Guid.Parse("3dd49e7f-7721-4673-9b6a-db11ecb36919");

            //tegevus
            await Svc<ISpaceshipServices>().DetailAsync(seekGuid);

            //assert
            Assert.Equal(databaseGuid, seekGuid);
        }


        //selles testis kontollitakse, et spaceship kustutamisel db-st peaks kustuma objekt, kui tagastatav väärtus == tagastatav väärtus
        [Fact]

        public async Task Should_DeleteSpaceshipById_WhenReturnedResultIsEqual()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();

            //tegevus
            var addSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)addSpaceship.Id);

            //kontroll
            Assert.Equal(addSpaceship.Id, deleteSpaceship.Id);
        }


        [Fact]
        public async Task ShouldNot_DeleteSpaceshipByID_WhenDidNotDeleteSpaceship()
        {
            //ülesseade
            var dto = MockSpaceshipData();

            //tegevus
            var spaceShip1 = await Svc<ISpaceshipServices>().Create(dto);
            var spaceShip2 = await Svc<ISpaceshipServices>().Create(dto);

            var result = await Svc<ISpaceshipServices>().Delete((Guid)spaceShip2.Id);

            //kontroll
            Assert.NotEqual(spaceShip1.Id, result.Id);
        }


        //test, mis kontrollib, et andmeid uuendatakse update korral
        [Fact]
        public async Task Should_UpdateSpaceshipByID_WhenUpdatingData()
        {
            //ülesseade
            var Guid = new Guid("da1341fe-f558-49f9-b4c1-eb9b9b07fb87");

            SpaceshipDto dto = MockSpaceshipData();

            SpaceshipDto domain = new();

            domain.Id = Guid;
            domain.EnginePower = 2000;
            domain.Name = "Igor Mang";
            domain.ShipType = "cosmic";
            domain.Crew = 34;
            domain.CreatedAt = dto.CreatedAt;
            domain.UpdatedAt = DateTime.Now;

            //tegevus
            await Svc<ISpaceshipServices>().Update(dto);

            //kontroll
            Assert.Equal(domain.Id, Guid);
            Assert.NotEqual(dto.EnginePower, domain.EnginePower);
            Assert.NotEqual(dto.Name, domain.Name);
            Assert.DoesNotMatch(dto.Crew.ToString(), domain.Crew.ToString());
            Assert.DoesNotMatch(dto.ShipType, domain.ShipType);
            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.UpdatedAt, domain.UpdatedAt);

        }


        [Fact]
        public async Task ShouldNot_UpdateSpaceshipById_WhenNoDataUpdated()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();
            var createdSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            //tegevus
            SpaceshipDto nullDto = MockSpaceshipNullData();
            var result = await Svc<ISpaceshipServices>().Update(nullDto);

            //kontroll
            Assert.NotEqual(createdSpaceship.Id, result.Id);
                
        }


        //kna mootor ei saa olla negatiivse võimsusega, kontrollime, et ei saaks lisaga negatiivset
        [Fact]
        public async Task ShouldNot_CreateSpaceship_WhenEnginepowerNegative()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData(true);
            dto.EnginePower -= (dto.EnginePower * 2);

            //tegevus 
            var result = await Svc<ISpaceshipServices>().Create(dto);

            //kontroll
            Assert.True(result.EnginePower > 0);
        }


        //test, mis kontrollib, et meeskond > 3, service ei tohi lisada vähemat, aga 
        //võib lahendada ükskõik kuidas
        [Fact]
        public async Task ShouldNot_CreateSpaceship_WhenCrewIsLessThanThree()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData(true);
            dto.Crew = 0;

            //tegevus
            var result = await Svc<ISpaceshipServices>().Create(dto);

            //kontroll
            Assert.True(result.Crew > 3);
        }

        [Fact]
        public async Task Should_RemoveSpaceshipFromDatabase_WhenSpaceshipIsDeleted()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();

            //tegevus
            var createdSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deletedSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)createdSpaceship.Id);
            var result = await Svc<ISpaceshipServices>().DetailAsync((Guid)createdSpaceship.Id);

            //konrtoll
            Assert.Equal(createdSpaceship.Id, deletedSpaceship.Id);
            Assert.Null(result);
        }


        [Fact]
        public async Task ShouldNot_RemoveSpaceshipFromDatabase_WhenSpaceshipIdIsdifferent()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();

            //tegevus
            var spaceShip1 = await Svc<ISpaceshipServices>().Create(dto);
            var spaceShip2 = await Svc<ISpaceshipServices>().Create(dto);
            var deletedSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)spaceShip2.Id);
            var result = await Svc<ISpaceshipServices>().DetailAsync((Guid)spaceShip1.Id);
            

            //kontroll
            Assert.NotEqual(spaceShip1.Id, spaceShip2.Id);
            Assert.NotNull(result);
        }

        





        //üleval testid, all abimeetodid

        /// <summary>
        /// tagastab null-objekti testi läbiviimiseks
        /// </summary>
        /// <returns></returns>
        private SpaceshipDto MockSpaceshipNullData()
        {
            return new SpaceshipDto
            {
                Id = null,
                Name = "",
                ShipType = "",
                Crew = 0,
                EnginePower = 0,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };
        }

        private SpaceshipDto MockSpaceshipData(bool isOneOrTwo = false)
        {
            if (isOneOrTwo == false)
            {
                return new SpaceshipDto
                {
                    Name = "X Space",
                    ShipType = "rakett",
                    Crew = 2,
                    EnginePower = 12,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            }
            else
            {
                return new SpaceshipDto
                {
                    Name = "Fallen Eagle",
                    ShipType = "droon",
                    Crew = 22,
                    EnginePower = 369,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            }
        }


    }
}
