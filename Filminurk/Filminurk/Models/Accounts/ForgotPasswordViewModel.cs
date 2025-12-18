using System.ComponentModel.DataAnnotations;
namespace Filminurk.Models.Accounts
{
    public class forgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
