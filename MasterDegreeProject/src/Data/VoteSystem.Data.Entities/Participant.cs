using System;

using VoteSystem.Data.Entities.Contracts;

namespace VoteSystem.Data.Entities
{
    public class Participant : IAuditInfo
    {
        public int Id { get; set; }

        public bool IsVoted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid VoteSystemUserId { get; set; }
        public virtual VoteSystemUser VoteSystemUser { get; set; }

        public Guid VoteSystemId { get; set; }
        public virtual VoteSystem VoteSystem { get; set; }
    }
}
