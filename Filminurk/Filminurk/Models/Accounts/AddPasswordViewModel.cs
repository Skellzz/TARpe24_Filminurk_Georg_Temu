using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class AddPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sisesta uus parool")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Kirjuta oma uus parool uuesti:")]
        [Compare("NewPassword", ErrorMessage = "Paroolid ei kattu.")]
        public string ConfirmNewPassword { get; set; }
    }
}
