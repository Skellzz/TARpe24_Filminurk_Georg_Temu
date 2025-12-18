using Azure.Identity;
using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    internal class AccountsServices : IAccountsServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailsServices _emailsServices;

        public AccountsServices(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailsServices emailsServices
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailsServices = emailsServices;
        }

        public async Task<ApplicationUser> Register(ApplicationUserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                UserName = userDTO.Email,
                EmailsServices = userDTO.Email,
                ProfileType = userDTO.ProfileType,
                DisplayNameAttribute = userDTO.DisplayName

            };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //Homework location
            }
            return user;
        }

        // homewok location

    }

}
