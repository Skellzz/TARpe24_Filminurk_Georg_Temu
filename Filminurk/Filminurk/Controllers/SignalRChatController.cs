using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class SignalRChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
