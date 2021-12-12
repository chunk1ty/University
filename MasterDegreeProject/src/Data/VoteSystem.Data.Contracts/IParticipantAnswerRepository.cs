using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Contracts
{
    public interface IParticipantAnswerRepository
    {
        void Add(ParticipantAnswer participantAnswer);
    }
}
