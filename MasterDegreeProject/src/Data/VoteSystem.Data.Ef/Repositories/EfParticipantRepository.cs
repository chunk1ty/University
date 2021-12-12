using System.Collections.Generic;
using System.Data.Entity;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Repositories
{
    public class EfParticipantRepository : IParticipantRepository
    {
        private readonly IVoteSystemDbContext _voteSystemDbContext;

        public EfParticipantRepository(IVoteSystemDbContext voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }

        public void Add(Participant participant)
        {
            var entry = _voteSystemDbContext.Entry(participant);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _voteSystemDbContext.Participants.Add(participant);
            }
        }

        public void Update(Participant participant)
        {
            var entry = _voteSystemDbContext.Entry(participant);

            if (entry.State == EntityState.Detached)
            {
                _voteSystemDbContext.Set<Participant>().Attach(participant);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(Participant participant)
        {
            var entry = _voteSystemDbContext.Entry(participant);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _voteSystemDbContext.Set<Participant>().Attach(participant);
                _voteSystemDbContext.Set<Participant>().Remove(participant);
            }
        }

        public IEnumerable<Participant> All()
        {
            return _voteSystemDbContext.Participants;
        }

        public IEnumerable<Participant> AllWithVoteSystemUser()
        {
            return _voteSystemDbContext.Participants
                                                .Include(x => x.VoteSystemUser);
        }
    }
}
