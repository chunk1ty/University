using System;
using System.Collections.Generic;

namespace VoteSystem.Data.Services.Contracts
{
    public interface IVoteSystemService
    {
        void Add(Entities.VoteSystem system);

        void Delete(Guid voteSystemId);

        void Update(Entities.VoteSystem system);

        IEnumerable<Entities.VoteSystem> All();

        IEnumerable<Entities.VoteSystem> GetAllAvailableVoteSystemsForUserByUserId(Guid userId);

        Entities.VoteSystem GetById(Guid voteSystemId);
    }
}
