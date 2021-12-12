using System;
using System.Collections.Generic;

using VoteSystem.Data.Entities;
using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services.Contracts
{
    public interface IQuestionService
    {
        void AddRange(IList<Question> questions);

        void UpdateRange(IList<Question> questions);

        IEnumerable<Question> GetQuestionsWithAnswersByVoteSystemId(Guid voteSystemId);

        IEnumerable<QuestionResultDto> GetQuestionResultByVoteSystemId(Guid voteSystemId);
    }
}
