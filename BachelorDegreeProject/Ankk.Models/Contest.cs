namespace Ankk.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contest
    {
        public  Contest()
        {
            this.Users = new HashSet<User>();
            this.Questions = new HashSet<Question>();            
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        [Required]
        public bool IsStrict { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
