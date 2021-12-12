using System;
using System.Collections.Generic;

namespace VoteSystem.Data.Contracts
{
    public interface IVoteSystemRepository
    {
        void Add(Entities.VoteSystem system);

        void Delete(Entities.VoteSystem voteSystem);

        void Update(Entities.VoteSystem voteSystem);

        Entities.VoteSystem GetById(Guid voteSystemId);

        IEnumerable<Entities.VoteSystem> GetAll();

        IEnumerable<Entities.VoteSystem> GetAllWithParticipants();
    }
}
