using System;
using System.Collections.Generic;
using VoteSystem.Data.Entities.Contracts;

namespace VoteSystem.Data.Entities
{
    public class Question : IAuditInfo, IDeletableEntity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool HasMultipleAnswers { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid VoteSystemId { get; set; }
        public virtual VoteSystem VoteSystem { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
