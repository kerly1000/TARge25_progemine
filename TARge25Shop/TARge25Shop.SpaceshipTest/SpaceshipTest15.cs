using System;
using System.Collections.Generic;
using System.Text;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop.SpaceshipTest
{
    public class SpaceshipTest15 : TestBase
    {
        [Fact] //1
        public async Task Should_AddSpaceShip_WhenResultIsReturned()
        {
            // Ülesseade
            SpaceshipDto dto = new SpaceshipDto()
            {
                Name = "X AE a L 12 menuornvöerv",
                ShipType = "lendav taldrik",
                Crew = 67,
                EnginePower = 69,//hobujõudu siis
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            // tegutsemine
            var result = await Svc<ISpaceshipServices>().Create(dto);

            // kontroll
            Assert.NotNull(result);
        }

        [Fact] //2
        public async Task ShouldNot_GetSpaceshipByID_WhenGuidIsNotEqual()
        {
            // ülessezade
            Guid databaseGuid = Guid.Parse("ecbc059a-0bca-4df2-aae9-a3211e69185a");
            Guid seekGuid = Guid.Parse("ecbc059a-0bca-4df2-aae7-a3211e69185a");

            // tegevus
            await Svc<ISpaceshipServices>().DetailAsync(seekGuid);

            // kontroll
            Assert.NotEqual(databaseGuid, seekGuid);
        }

        [Fact]
        public async Task ShouldNot_DeleteSpaceshipByID_WhenReturnedResultIsNotEqual()
        {
            var dto = MockSpaceshipData();
            var createdSpaceship1 = await Svc<ISpaceshipServices>().Create(dto);
            var createdSpaceship2 = await Svc<ISpaceshipServices>().Create(dto);
            var deleteResult = await Svc<ISpaceshipServices>().Delete((Guid)createdSpaceship2.Id);
            var spaceshipindb = await Svc<ISpaceshipServices>().DetailAsync((Guid)createdSpaceship1.Id);

            //kontroll
            Assert.NotNull(spaceshipindb);
            Assert.NotEqual(deleteResult.Id, createdSpaceship1.Id);
            Assert.Equal(createdSpaceship1, spaceshipindb);
        }




        //abimeetodid
        private SpaceshipDto MockSpaceshipData(bool isOneOrTwo = false)
        {
            if (isOneOrTwo == false)
            {
                return new SpaceshipDto
                {
                    Name = "X AE a L 12 menuornvöerv",
                    ShipType = "lendav taldrik",
                    Crew = 67,
                    EnginePower = 69,//hobujõudu siis
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
            }
            else
            {
                return new SpaceshipDto
                {
                    Name = "RAKETT69",
                    ShipType = "lendav kauss",
                    Crew = 420,
                    EnginePower = 999,//hobujõudu siis
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
            }
        }
    }
}
