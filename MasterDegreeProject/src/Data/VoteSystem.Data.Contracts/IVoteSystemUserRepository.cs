using System.Collections.Generic;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Contracts
{
    public interface IVoteSystemUserRepository
    {
        IEnumerable<VoteSystemUser> GetWithParticipnats();
    }
}
