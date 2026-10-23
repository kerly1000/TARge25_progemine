using TARge25Shop.Models;

namespace TARge25Shop.Models.OpenWeather
{
    public class OpenWeatherSearchViewModel
    {
        public string? SearchCity { get; set; }
        public List<OpenWeatherViewModel>? Weathers { get; set; }
            = new List<OpenWeatherViewModel> ();
    }
}

