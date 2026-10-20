using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TARge25Shop.Core.Dto;

namespace TARge25Shop.ApplicationServices.Services
{
    public class OpenWeatherServices
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenWeatherRootDto> OpenWeatherResultHttpClient()
        {
            var apiKey = "c55d132af5e1428b5112783558448031";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={{city%20name}}&appid={{API%20key}}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var weather = JsonSerializer.Deserialize<OpenWeatherRootDto>(
                json, options);

            return weather!;
        }
    }
}
