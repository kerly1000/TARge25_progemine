using System;
using System.Collections.Generic;

namespace TARge25Shop.Models.OpenWeather
{
    public class OpenWeatherViewModel
    {
        public CoordViewModel? Coord { get; set; }
        public List<WeatherViewModel>? Weather { get; set; }
        public string? Base { get; set; }
        public MainViewModel? Main { get; set; }
        public int Visibility { get; set; }
        public WindViewModel? Wind { get; set; }
        public CloudsViewModel? Clouds { get; set; }
        public int Dt { get; set; }
        public SysViewModel? Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Cod { get; set; }

        // Abimeetod või omadus, kui soovid temperatuuri otse Celsiuses näidata (OpenWeather annab standardina Kelvinid, kui units pole määratud, või Celsiuse kui units=metric)
        public string FormattedTemp => Main != null ? $"{Math.Round(Main.Temp)} °C" : string.Empty;
    }

    public class CoordViewModel
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class WeatherViewModel
    {
        public int Id { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }

        // Abimeetod ilmaikooni täieliku URL-i saamiseks (OpenWeather standard)
        public string IconUrl => !string.IsNullOrEmpty(Icon) ? $"https://openweathermap.org/img/wn/{Icon}@2x.png" : string.Empty;
    }

    public class MainViewModel
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindViewModel
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    public class CloudsViewModel
    {
        public int All { get; set; }
    }

    public class SysViewModel
    {
        public string? Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        // Mugavusomadus Unix timestampi teisendamiseks loetavaks kellaajaks
        public DateTime SunriseTime => DateTimeOffset.FromUnixTimeSeconds(Sunrise).LocalDateTime;
        public DateTime SunsetTime => DateTimeOffset.FromUnixTimeSeconds(Sunset).LocalDateTime;
    }
}