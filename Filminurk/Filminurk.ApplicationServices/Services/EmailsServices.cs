using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace Filminurk.ApplicationServices.Services
{
    public class EmailsServices : IEmailsServices
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailsServices(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SendEmail(EmailDTO dto)
        {
            var email = new MimeMessage();
            _configuration.GetSection("EmailUserName").Value = Enviroment.gmailusername;
            _configuration.GetSection("EmailHost").Value = Enviroment.smtppaddress;
            _configuration.GetSection("EmailPassword").Value = Enviroment.gmailiapppassword;

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.SendToThisAddress));
            email.Subject = dto.EmailSubject;
            var builder = new BodyBuilder
            {
                HtmlBody = dto.EmailContent,
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
        public async Task SendConfirmationEmailAsync(ApplicationUser user, string scheme)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Email)) throw new InvalidOperationException("User email missing.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var request = _httpContextAccessor.HttpContext?.Request
                ?? throw new InvalidOperationException("HTTP context missing.");

            var host = request.Host.Value;


            var encodedToken = System.Net.WebUtility.UrlEncode(token);
            var encodedUserId = System.Net.WebUtility.UrlEncode(user.Id);

            var confirmationLink =
                $"{scheme}://{host}/Accounts/ConfirmEmail?userId={encodedUserId}&token={encodedToken}";

            var dto = new EmailDTO
            {
                SendToThisAddress = user.Email,
                EmailSubject = "Email Confirmation",
                EmailContent = "Please click this link to confirm your account: " + confirmationLink
            };

            SendEmail(dto);
        }
    }
}
