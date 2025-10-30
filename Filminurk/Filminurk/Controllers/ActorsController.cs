using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
