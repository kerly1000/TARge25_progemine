using System;
using System.Collections.Generic;
using System.Text;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using static TARge25Shop.Core.Dto.KindergartenDto;

namespace TARge25Shop.ApplicationServices.Services
{
    public class KindergartenServices
    {
        

            private readonly TARge25ShopContext _context;
           

            public KindergartenServices
                (
                    TARge25ShopContext context
                    

                )
            {
                _context = context;
                
            }

            //meetod tuleb controllerid esile kutsuda, st vaja liidestada

            public async Task<Core.Domain.Kindergarten> Create(KindergartenDto dto)
            {   //vaheinstants dto ja domain vahel, et andmed liiguks suunal
                //dto-domain
                Kindergarten kinderGarten = new();

                kinderGarten.Id = Guid.NewGuid();
                kinderGarten.GroupName = dto.GroupName;
                kinderGarten.ChildrenCount = dto.ChildrenCount;
                kinderGarten.KindergartenName = dto.KindergartenName;
                kinderGarten.TeacherName = dto.TeacherName;
                kinderGarten.CreatedAt = DateTime.Now;
                kinderGarten.UpdatedAt = DateTime.Now;
                //kui uus ankeet on loodud, siis toimub ka faili salvestamine
                //saab kutsuda teise service classi meetodit
                


                //andmete salvestamine andmebaasi
                _context.Kindergartens.Add(kinderGarten);
                await _context.SaveChangesAsync();


                return kinderGarten;
            }

            //teha update meetod, mis võtab vastu dto ja uuendab olemasolevad kosmoselaeva
            public async Task<Kindergarten> Update(KindergartenDto dto)
            {   //vaheinstants dto ja domain vahel, et andmed liiguks suunal
                //dto-domain
                Kindergarten kinderGarten = new();

                kinderGarten.Id = (Guid)dto.Id;
                kinderGarten.GroupName = dto.GroupName;
                kinderGarten.ChildrenCount = dto.ChildrenCount;
                kinderGarten.KindergartenName = dto.KindergartenName;
                kinderGarten.TeacherName = dto.TeacherName;
                kinderGarten.CreatedAt = dto.CreatedAt;
                kinderGarten.UpdatedAt = DateTime.Now;

                //andmete uuendamine andmebaasis
                _context.Kindergartens.Update(kinderGarten);
                await _context.SaveChangesAsync();


                return kinderGarten;
            }

            //public async Task<Kindergarten> DetailAsync(Guid id)
            //{
            //    var kindergarten = await _context.Kindergartens
            //        .FirstOrDefaultAsync(x => x.Id == id);

            //    return kindergarten;

            //}

            //public async Task<Kindergarten> Delete(Guid id)
            //{
            //    var result = await _context.Kindergartens
            //        .FirstOrDefaultAsync(x => x.Id == id);

            //    _context.Kindergartens.Remove(result);
            //    await _context.SaveChangesAsync();

            //    return result;
            //}

        
    }
}
