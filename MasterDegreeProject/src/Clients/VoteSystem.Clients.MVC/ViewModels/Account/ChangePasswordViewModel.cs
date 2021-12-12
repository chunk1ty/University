using System.ComponentModel.DataAnnotations;

namespace VoteSystem.Clients.MVC.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Стара парола")]
        [Required(ErrorMessage = "Въведете стара парола.")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        [Required(ErrorMessage = "Въведете нова парола")]
        [StringLength(100, ErrorMessage = "Паролата трябва да е най-малко {2} символа.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтори новата парола")]
        [Required(ErrorMessage = "Потвърдете новата парола.")]
        [Compare("NewPassword", ErrorMessage = "Паролите несъвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}