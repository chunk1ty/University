namespace Ankk.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
