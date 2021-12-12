using System;
using System.Collections.Generic;

using VoteSystem.Data.Entities;
using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services.Contracts
{
    public interface IParticipantService
    {
        void AddRange(VoteSystemParticipantsDto voteSystemParticipants);

        void RemoveRange(IEnumerable<Participant> participants);

        void Update(Participant participant);

        Participant GetParticipantByVoteSystemIdAndVoteSystemUserId(Guid voteSystemId, Guid userId);

        IEnumerable<Participant> GetParticipantsByVoteSystemId(Guid voteSystemId);
    }
}
