using System;
using System.Collections.Generic;
using System.Data.Entity;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;

namespace VoteSystem.Data.Ef.Repositories
{
    public class EfVoteSystemRepository : IVoteSystemRepository
    {
        private readonly IVoteSystemDbContext _voteSystemDbContext;

        public EfVoteSystemRepository(IVoteSystemDbContext voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }

        public void Add(Entities.VoteSystem system)
        {
            var entry = _voteSystemDbContext.Entry(system);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _voteSystemDbContext.VoteSystems.Add(system);
            }
        }

        public void Delete(Entities.VoteSystem voteSystem)
        {
            var entry = _voteSystemDbContext.Entry(voteSystem);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _voteSystemDbContext.Set<Entities.VoteSystem>().Attach(voteSystem);
                _voteSystemDbContext.Set<Entities.VoteSystem>().Remove(voteSystem);
            }
        }

        public void Update(Entities.VoteSystem voteSystem)
        {
            var entry = _voteSystemDbContext.Entry(voteSystem);

            if (entry.State == EntityState.Detached)
            {
                _voteSystemDbContext.Set<Entities.VoteSystem>().Attach(voteSystem);
            }

            entry.State = EntityState.Modified;
            entry.Property(x => x.CreatedOn).IsModified = false;
        }

        public Entities.VoteSystem GetById(Guid voteSystemId)
        {
            return _voteSystemDbContext.VoteSystems.Find(voteSystemId);
        }

        public IEnumerable<Entities.VoteSystem> GetAll()
        {
            return _voteSystemDbContext.VoteSystems;
        }

        public IEnumerable<Entities.VoteSystem> GetAllWithParticipants()
        {
            return _voteSystemDbContext.VoteSystems
                .Include(x => x.Participants);
        }
    }
}
