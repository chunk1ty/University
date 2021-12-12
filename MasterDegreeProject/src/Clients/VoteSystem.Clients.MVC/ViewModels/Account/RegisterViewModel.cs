using System.ComponentModel.DataAnnotations;

namespace VoteSystem.Clients.MVC.ViewModels.Account
{
    public class RegisterViewModel
    {        
        [Required(ErrorMessage = "Имeйла е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [MinLength(2, ErrorMessage = "Името не може да е по-малко от 2 букви.")]
        [MaxLength(20, ErrorMessage = "Името не може да е по-голямо от 20 букви.")]        
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [MinLength(2, ErrorMessage = "Фамилията не може да е по-малка от 2 букви.")]
        [MaxLength(20, ErrorMessage = "Фамилията не може да е по-голяма от 20 букви.")]        
        public string LastName { get; set; }

        [Required(ErrorMessage = "Факултетният номер е задължителен.")]
        [Range(10000, 9999999, ErrorMessage = "Факултетният номер трябва да е между 10000 и 9999999")]        
        public int FacultyNumber { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, ErrorMessage = "Паролата трябва да е с дължина минимум {2} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [DataType(DataType.Password)]       
        [Compare("Password", ErrorMessage = "Паролата несъвпада.")]
        public string ConfirmPassword { get; set; }
    }
}