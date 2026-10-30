using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using TARge25Shop.Models.Kindergarten;

using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace TARge25Shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly IKindergartenServices _kinderGartenServices;
        private readonly TARge25ShopContext _context;
        

        public KindergartenController
            (
                IKindergartenServices kinderGartenServices,
                TARge25ShopContext context
                
            )
        {
            _kinderGartenServices = kinderGartenServices;
            _context = context;
            
        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                
            };

            var result = await _kinderGartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            

            var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
            
            };

            var result = await _kinderGartenServices.Update(dto);

            var kinderGartenId = result.Id;

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { id = kinderGartenId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }


            var vm = new KindergartenDeleteViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.Delete(id);

            if (kinderGarten == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kinderGarten = await _kinderGartenServices.DetailAsync(id);

            if (kinderGarten == null)
            {
                return NotFound();
            }

            

            var vm = new KindergartenDetailViewModel();

            vm.Id = kinderGarten.Id;
            vm.GroupName = kinderGarten.GroupName;
            vm.ChildrenCount = kinderGarten.ChildrenCount;
            vm.KindergartenName = kinderGarten.KindergartenName;
            vm.TeacherName = kinderGarten.TeacherName;
            vm.CreatedAt = kinderGarten.CreatedAt;
            vm.UpdatedAt = kinderGarten.UpdatedAt;
            

            return View(vm);
        }

        

        
    }




}