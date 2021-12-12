using System;
using System.Collections.Generic;
using VoteSystem.Data.Entities.Contracts;

namespace VoteSystem.Data.Entities
{
    public class Answer : IAuditInfo
    {
        public Answer()
        {
            ParticipantAnswers = new HashSet<ParticipantAnswer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public ICollection<ParticipantAnswer> ParticipantAnswers { get; set; }
    }
}
