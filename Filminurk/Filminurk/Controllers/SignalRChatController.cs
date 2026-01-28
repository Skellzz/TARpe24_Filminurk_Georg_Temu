using Filminurk.Core.Domain;
using Filminurk.Models.Accounts;
using Filminurk.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    [Authorize]
    public class SignalRChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SignalRChatController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Accounts");

            var vm = new ChatViewModel
            {
                DisplayName = user.DisplayName ?? user.UserName
            };

            return View(vm);
        }
    }
}
