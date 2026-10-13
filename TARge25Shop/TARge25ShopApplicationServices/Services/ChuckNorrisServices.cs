using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;

namespace TARge25Shop.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisServices
            (
            HttpClient httpClient
            )
        {
            _httpClient = httpClient;
        }
        public async Task<ChuckNorrisRootDto> ChuckNorrisResultHttpClient()
        {

            //var response on vaja ühendada https://api.chucknorris.io/jokes/random
            var response = await _httpClient.GetAsync("https://api.chucknorris.io/jokes/random");
            //annab veateate, kui response ei ole edukas
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {   
                //olek true laseb igoreerida täpset tähe suurustundlikkust JSON muutujate nimes
                PropertyNameCaseInsensitive = true
            };

            //muutuja var joke tuleb deserialiseerida
            var joke = JsonSerializer.Deserialize<ChuckNorrisRootDto>(json, options);

            //nulli andestav operaator(!) on turvaline viis, kuna API alati tagastab JSONit
            return joke!;
            
        }

    }
}
