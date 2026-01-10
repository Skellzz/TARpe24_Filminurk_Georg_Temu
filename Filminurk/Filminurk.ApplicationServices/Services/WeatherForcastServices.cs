using Filminurk.Core.Dto.AccuWeather;
using Filminurk.Core.ServiceInterface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using System.Text.Json;

namespace Filminurk.ApplicationServices.Services
{
    public class WeatherForcastServices : IWeatherForcastServices
    {
        public async Task<AccuLocationWeatherResultDTO> AccuWeatherResult(AccuLocationWeatherResultDTO dto)
        {
            string apiKey = Enviroment.accuweatherkey; //key tuleb enviromentitśt, ega pole hardcodetud
            var baseUrl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = await httpClient.GetAsync($"{dto.CityCode}?apikey={apiKey}&details=true");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                List<AccuCityCodeRootDTO> weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDTO>>(jsonResponse);
                dto.CityName = weatherData[0].LocalizedName;
                dto.CityCode = weatherData[0].Key;
            }
            string weatherResponse = baseUrl + $"{dto.CityCode}?apikey={apiKey}&metric=true";

            using (var clientWeather = new HttpClient())
            { 
                var httpResponse = await clientWeather.GetAsync(weatherResponse);
                string jsonWeatherResponse = await httpResponse.Content.ReadAsStringAsync();

                AccuLocationRootDTO weatherRootDTO = JsonSerializer.Deserialize<AccuLocationRootDTO>(jsonWeatherResponse);

                dto.EffectiveDate = weatherRootDTO.Headline.EffectiveDate.ToString();
                dto.EffectiveEpochDate = weatherRootDTO.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.Text = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate.ToString();
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate.ToString();
                dto.MobileLink = weatherRootDTO.Headline.MobileLink;
                dto.Link = weatherRootDTO.Headline.Link;
                dto.DailyForecasts = JsonSerializer.Serialize(weatherRootDTO.DailyForecasts[0]);
                dto.DailyForecastsEpochDate = weatherRootDTO.DailyForecasts[0].EpochDate;
                dto.TempMinValue = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Unit;
                
            }
            return dto;
        }
    }
}
