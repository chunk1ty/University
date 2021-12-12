using System;
using System.Collections.Generic;

using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services.Contracts
{
    public interface IParticipantAnswerService
    {
        void Add(IList<ParticipantQuestionAnswerDto> participantQuestionAnswers, Guid userId);
    }
}
