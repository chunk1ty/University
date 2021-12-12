using System.Collections.Generic;
using System.Data.Entity;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Repositories
{
    public class EfVoteSystemUserRepository : IVoteSystemUserRepository
    {
        private readonly IVoteSystemDbContext _voteSystemDbContext;

        public EfVoteSystemUserRepository(IVoteSystemDbContext voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }

        public IEnumerable<VoteSystemUser> GetWithParticipnats()
        {
            return _voteSystemDbContext.VoteSystemUsers
                .Include(x => x.Participants);
        }
    }
}
