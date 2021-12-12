using System.Data.Entity;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Repositories
{
    public class EfParticipantAnswerRepository : IParticipantAnswerRepository
    {
        private readonly IVoteSystemDbContext _voteSystemDbContext;

        public EfParticipantAnswerRepository(IVoteSystemDbContext voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }
        
        public void Add(ParticipantAnswer participantAnswer)
        {
            var entry = _voteSystemDbContext.Entry(participantAnswer);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _voteSystemDbContext.ParticipantAnswers.Add(participantAnswer);
            }
        }
    }
}
