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

        [Fact]

        public async Task Should_DeleteSpaceshipById_WhenReturnedResultIsEqual()
        {
            //ülesseade
            SpaceshipDto dto = MockSpaceshipData();

            //tegevus
            var addSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete(addSpaceship.Id);

            //kontroll
            Assert.Equal(addSpaceship.Id, deleteSpaceship.Id);
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
