

using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;

namespace TARge25Shop.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices

    {

        private readonly TARge25ShopContext _context;

        public SpaceshipServices
            (
                TARge25ShopContext context
            )
        {
            _context = context;
        }

        //meetod tuleb controllerid esile kutsuda, st vaja liidestada

        public async Task<Spaceship> Create(SpaceshipDto dto)
        {   //vaheinstants dto ja domain vahel, et andmed liiguks suunal
            //dto-domain
            Spaceship spaceShip = new();
            
                spaceShip.Id = dto.Id;
                spaceShip.Name = dto.Name;
                spaceShip.ShipType = dto.ShipType;
                spaceShip.Crew = dto.Crew;
                spaceShip.EnginePower = dto.EnginePower;
                spaceShip.CreatedAt = dto.CreatedAt;
                spaceShip.UpdatedAt = dto.UpdatedAt;

            //andmete salvestamine andmebaasi
            _context.Spaceships.Add(spaceShip);
            await _context.SaveChangesAsync();


            return spaceShip;
        }
    }

}
