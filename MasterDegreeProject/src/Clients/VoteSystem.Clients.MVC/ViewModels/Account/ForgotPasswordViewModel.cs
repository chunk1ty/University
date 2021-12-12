using System.ComponentModel.DataAnnotations;

namespace VoteSystem.Clients.MVC.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Имeйла е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        public string Email { get; set; }
    }
}