namespace Ankk.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "My Code")]
        public string SourceCode { get; set; }

        public int Points { get; set; }

        public string UsersId { get; set; }

        public int QuestionId { get; set; }

        public QuestionViewModel Questions { get; set; }
    }
}