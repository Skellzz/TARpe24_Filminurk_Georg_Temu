using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.Emails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Filminurk.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailsServices _emailServices;
        public EmailsController(IEmailsServices emailServices)
        {
            _emailServices = emailServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            var dto = new EmailDTO()
            {
                SendToThisAddress = vm.SendToThisAddress,
                EmailSubject = vm.EmailSubject,
                EmailContent = vm.EmailContent
                
            };
            _emailServices.SendEmail(dto);
            return RedirectToAction(nameof(Index));
        }

        //HOMEWORK LOCATION
    }
}
