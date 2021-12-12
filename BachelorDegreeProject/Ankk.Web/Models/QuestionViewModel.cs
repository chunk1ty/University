namespace Ankk.Web.Models
{
    using Ankk.Models;
    using System.ComponentModel.DataAnnotations;

    public class QuestionViewModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public int ContestId { get; set; }

        public int AnswerId { get; set; }

        public AnswerViewModel Answer { get; set; }

        public ContestViewModel Contest { get; set; }
    }
}