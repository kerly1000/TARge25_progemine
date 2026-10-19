using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Models.Cocktail;

namespace TARge25Shop.Controllers
{
    public class CocktailController : Controller
    {
        private readonly ICocktailServices _cocktailServices;

        public CocktailController(ICocktailServices cocktailServices)
        {
            _cocktailServices = cocktailServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View("Index");
            }

            var cocktails =
                await _cocktailServices.CocktailResultHttpClient(name);

            var vm = new CocktailSearchViewModel
            {
                SearchName = name,
                Cocktails = cocktails.Drinks?
                    .Select(c => new CocktailViewModel
                    {
                        Id = c.IdDrink,
                        Name = c.StrDrink,
                        Category = c.StrCategory,
                        Instructions = c.StrInstructions,
                        ImageUrl = c.StrDrinkThumb,
                        Glass = c.StrGlass,
                        Ingredient1 = c.StrIngredient1,
                        Ingredient2 = c.StrIngredient2,
                        Ingredient3 = c.StrIngredient3,
                        Ingredient4 = c.StrIngredient4,
                        Measure1 = c.StrMeasure1,
                        Measure2 = c.StrMeasure2,
                        Measure3 = c.StrMeasure3,
                        Measure4 = c.StrMeasure4
                    })
                    .ToList() ?? new List<CocktailViewModel>()
            };

            return View("Search", vm);
        }
    }
}