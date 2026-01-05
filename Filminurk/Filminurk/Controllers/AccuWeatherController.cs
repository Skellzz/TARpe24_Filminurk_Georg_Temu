using Filminurk.Core.ServiceInterface;
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
    }
}
