namespace Ankk.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        //public Answer()
        //{
        //    this.Questions = new HashSet<Question>();
        //}

        public int Id { get; set; }
        
        public string SourceCode { get; set; }

        public int Points { get; set; }

        //public virtual ICollection<Question> Questions { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Questions { get; set; }

        public string UsersId { get; set; }

        public virtual User Users { get; set; }
    }
}
