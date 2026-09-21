
using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;

namespace TARge25Shop.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices

    {

        private readonly TARge25ShopContext _context;
        private readonly IFileServices _fileServices;

        public SpaceshipServices
            (
                TARge25ShopContext context,
                IFileServices fileServices

            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        //meetod tuleb controllerid esile kutsuda, st vaja liidestada

        public async Task<Spaceship> Create(SpaceshipDto dto)
        {   //vaheinstants dto ja domain vahel, et andmed liiguks suunal
            //dto-domain
            Spaceship spaceShip = new();
            
            spaceShip.Id = Guid.NewGuid();
            spaceShip.Name = dto.Name;
            spaceShip.ShipType = dto.ShipType;
            spaceShip.Crew = dto.Crew;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.CreatedAt = DateTime.Now;
            spaceShip.UpdatedAt = DateTime.Now;
            //kui uus ankeet on loodud, siis toimub ka faili salvestamine
            //saab kutsuda teise service classi meetodit
            _fileServices.FilesToApi(dto, spaceShip);


            //andmete salvestamine andmebaasi
            _context.Spaceships.Add(spaceShip);
            await _context.SaveChangesAsync();


            return spaceShip;
        }

        //teha update meetod, mis võtab vastu dto ja uuendab olemasolevad kosmoselaeva
        public async Task<Spaceship> Update(SpaceshipDto dto)
        {   //vaheinstants dto ja domain vahel, et andmed liiguks suunal
            //dto-domain
            Spaceship spaceShip = new();

            spaceShip.Id = (Guid)dto.Id;
            spaceShip.Name = dto.Name;
            spaceShip.ShipType = dto.ShipType;
            spaceShip.Crew = dto.Crew;
            spaceShip.EnginePower = dto.EnginePower;
            spaceShip.CreatedAt = dto.CreatedAt;
            spaceShip.UpdatedAt = DateTime.Now;

            //andmete uuendamine andmebaasis
            _context.Spaceships.Update(spaceShip);
            await _context.SaveChangesAsync();


            return spaceShip;
        }

        public async Task<Spaceship> DetailAsync(Guid id)
        {
            var spaceship = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return spaceship;

        }

        public async Task<Spaceship> Delete(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Spaceships.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

    }

}
