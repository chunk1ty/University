using System;
using System.Collections.Generic;
using System.Linq;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Services.Contracts;
using VoteSystem.Services.Web.Contracts;
using VotySystem.Data.DTO;

namespace VoteSystem.Data.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IVoteSystemEfDbContextSaveChanges _dbContextSaveChanges;

        public QuestionService(
            IQuestionRepository questionRepository,
            IVoteSystemEfDbContextSaveChanges dbContextSaveChanges)
        {
            _questionRepository = questionRepository;
            _dbContextSaveChanges = dbContextSaveChanges;
        }

        // TODO bulk insert
        public void AddRange(IList<Question> questions)
        {
            if (questions == null)
            {
                throw new ArgumentNullException(nameof(questions));
            }

            foreach (var question in questions)
            {
                _questionRepository.Add(question);
            }

            _dbContextSaveChanges.SaveChanges();
        }

        public void UpdateRange(IList<Question> questions)
        {
            if (questions == null)
            {
                throw new ArgumentNullException(nameof(questions));
            }

            var firstQuestion = questions.FirstOrDefault();

            if (firstQuestion != null)
            {
                var voteSystemId = firstQuestion.VoteSystemId;
                var allExistingQuestions = GetQuestionsWithAnswersByVoteSystemId(voteSystemId);

                foreach (var question in allExistingQuestions)
                {
                    _questionRepository.Delete(question);
                }
            }

            foreach (var question in questions)
            {
                _questionRepository.Add(question);
            }

            _dbContextSaveChanges.SaveChanges();
        }

        public IEnumerable<Question> GetQuestionsWithAnswersByVoteSystemId(Guid voteSystemId)
        {
            return _questionRepository
                            .GetAllQuestionsWithAnswers()
                            .Where(x => x.VoteSystemId == voteSystemId && !x.IsDeleted);
        }

        public IEnumerable<QuestionResultDto> GetQuestionResultByVoteSystemId(Guid voteSystemId)
        {
            var result = _questionRepository
                                    .GetUsersAnswersByVoteSystemId(voteSystemId)
                                    .Select(x => new QuestionResultDto()
                                    {
                                        Name = x.Name,
                                        Type = x.HasMultipleAnswers,
                                        Answers = x.Answers.Select(
                                               y => new AnswerResultDto()
                                               {
                                                   Name = y.Name,
                                                   AnswerCount = y.ParticipantAnswers.Count
                                               })
                                    });

            return result;
        }
    }
}
