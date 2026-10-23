using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Core.Dto;
using TARge25Shop.Models.OpenWeather;

namespace TARge25Shop.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeatherController(IOpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }

        // 1. Avaleht (Index.cshtml) - näitab lihtsalt otsinguvormi
        [HttpGet]
        public IActionResult Index()
        {
            return View(new OpenWeatherSearchViewModel());
        }

        // 2. Otsingumootor (otsib ja suunab Search.cshtml vaatesse)
        [HttpGet]
        public async Task<IActionResult> Search(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return RedirectToAction("Index");
            }

            // Pärime andmed API-st
            OpenWeatherDto dto = await _openWeatherServices.OpenWeatherResultHttpClient(cityName);

            if (dto == null)
            {
                var emptyVm = new OpenWeatherSearchViewModel { SearchCity = cityName };
                return View("Search", emptyVm);
            }

            // Kaardistame andmed ViewModelisse
            var vm = new OpenWeatherSearchViewModel
            {
                SearchCity = cityName,
                Weathers = new List<OpenWeatherViewModel>
                {
                    new OpenWeatherViewModel
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Base = dto.Base,
                        Visibility = dto.Visibility,
                        Dt = dto.Dt,
                        Timezone = dto.Timezone,
                        Cod = dto.Cod,

                        Coord = dto.Coord == null ? null : new CoordViewModel
                        {
                            Lon = dto.Coord.Lon,
                            Lat = dto.Coord.Lat
                        },

                        Main = dto.Main == null ? null : new MainViewModel
                        {
                            Temp = dto.Main.Temp,
                            FeelsLike = dto.Main.FeelsLike,
                            Pressure = dto.Main.Pressure,
                            Humidity = dto.Main.Humidity
                        },

                        Wind = dto.Wind == null ? null : new WindViewModel
                        {
                            Speed = dto.Wind.Speed,
                            Deg = dto.Wind.Deg
                        },

                        Clouds = dto.Clouds == null ? null : new CloudsViewModel
                        {
                            All = dto.Clouds.All
                        },

                        Sys = dto.Sys == null ? null : new SysViewModel
                        {
                            Country = dto.Sys.Country,
                            Sunrise = dto.Sys.Sunrise,
                            Sunset = dto.Sys.Sunset
                        },

                        Weather = dto.Weather?.Select(w => new WeatherViewModel
                        {
                            Id = w.Id,
                            Main = w.Main,
                            Description = w.Description,
                            Icon = w.Icon
                        }).ToList()
                    }
                }
            };

            // Tagastame konkreetselt Search.cshtml vaate koos andmetega!
            return View("Search", vm);
        }
    }
}