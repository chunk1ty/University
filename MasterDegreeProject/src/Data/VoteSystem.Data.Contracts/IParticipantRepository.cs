using System.Collections.Generic;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Contracts
{
    public interface IParticipantRepository
    {
        void Add(Participant participant);

        void Update(Participant participant);

        void Delete(Participant participant);

        IEnumerable<Participant> All();

        IEnumerable<Participant> AllWithVoteSystemUser();
    }
}
