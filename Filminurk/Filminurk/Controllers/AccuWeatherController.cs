using Filminurk.Core.Dto.AccuWeather;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.AccuWeather;
using Filminurk.Models.AccWeather;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForcastServices _weatherForcastServices;
        public AccuWeatherController(IWeatherForcastServices weatherForcastServices)
        {
            _weatherForcastServices = weatherForcastServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindWeather(AccuWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeather", new { city = model.CityName });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult City(string City)
        {
            AccuLocationWeatherResultDTO dto = new();
            dto.CityName = City;
            _weatherForcastServices.AccuWeatherResult(dto);
            AccuWeatherViewModel vm = new();

            vm.EffectiveDate = dto.EffectiveDate;
            vm.EffectiveEpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.Category = dto.Category;
            vm.EndDate = dto.EndDate;
            vm.EndEpochDate = dto.EndEpochDate;
            vm.DailyForecastDate = dto.DailyForecastDate;
            vm.DailyForecastsEpochDate = dto.DailyForecastsEpochDate;

            vm.TempMinValue = dto.TempMinValue;
            vm.TempMinUnit = dto.TempMinUnit;
            vm.TempMinUnitType = dto.TempMinUnitType;

            vm.TempMaxValue = dto.TempMaxValue;
            vm.TempMaxUnit = dto.TempMaxUnit;
            vm.TempMaxUnitType = dto.TempMaxUnitType;

            vm.DayIcon = dto.DayIcon;
            vm.DayIconPhrase = dto.DayIconPhrase;
            vm.DayHasPrecipitation= dto.DayHasPrecipitation;
            vm.DayPrecipitationType = dto.DayPrecipitationType;
            vm.DayPrecipitationIntensity = dto.DayPrecipitationIntensity;

            vm.NightIcon = dto.NightIcon;
            vm.NightIconPhrase = dto.NightIconPhrase;
            vm.NightHasPrecipitation = dto.NightHasPrecipitation;
            vm.NightPrecipitationType = dto.NightPrecipitationType;
            vm.NightPrecipitationIntensity = dto.NightPrecipitationIntensity;

            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;
            return View(vm);



        }
    }
}
