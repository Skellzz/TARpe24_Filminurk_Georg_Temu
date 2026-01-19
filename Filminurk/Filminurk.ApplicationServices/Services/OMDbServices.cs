using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OMDbApiDTO;
using Filminurk.Core.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Filminurk.Core.Dto.OMDbApiDTO.OMDbApiMovieRootDTO;

namespace Filminurk.ApplicationServices.Services
{
    public class OmdbServices : IOMDbApiServices
    {
        

        public async Task<OMDbApiMovieResultDTO> OmdbapiResult(OMDbApiMovieResultDTO dto)
        {
            string apikey = Filminurk.Data.Enviroment.accuweatherkey;
            var baseUrl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day/";
            var cityUrl = $"https://dataservice.accuweather.com/locations/v1/cities/search";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(cityUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = httpClient.GetAsync($"?q={dto.Title}&apikey={apikey}&details=true").GetAwaiter().GetResult();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    List<Root> omdbData = JsonSerializer.Deserialize<List<Root>>(jsonResponse);
                    dto.Title = omdbData[0].Title;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string omdbResponse = baseUrl + $"{dto.Title}?apikey={apikey}";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseOmdb = clientWeather.GetAsync(omdbResponse).GetAwaiter().GetResult();
                string jsonOmdb = await httpResponseOmdb.Content.ReadAsStringAsync();

                Root omdbRootDTO = JsonSerializer.Deserialize<Root>(jsonOmdb);

            }
            return dto;
        }


        Movie IOMDbApiServices.Create(OMDbApiMovieCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<OMDbApiMovieResultDTO> IOMDbApiServices.OMDbApiResult(OMDbApiMovieResultDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
