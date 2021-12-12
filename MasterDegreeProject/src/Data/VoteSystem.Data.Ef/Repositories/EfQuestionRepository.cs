using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Repositories
{
    public class EfQuestionRepository : IQuestionRepository
    {
        private readonly IVoteSystemDbContext _voteSystemDbContext;

        public EfQuestionRepository(IVoteSystemDbContext voteSystemDbContext)
        {
            _voteSystemDbContext = voteSystemDbContext;
        }

        public void Add(Question question)
        {
            var entry = _voteSystemDbContext.Entry(question);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _voteSystemDbContext.Questions.Add(question);
            }
        }

        public void Delete(Question question)
        {
            // TODO: discuss that problem
            // The operation failed: The relationship could not be changed because one or more of the foreign-key properties is non-nullable. When a change is made to a relationship, the related foreign-key property is set to a null value. If the foreign-key does not support null values, a new relationship must be defined, the foreign-key property must be assigned another non-null value, or the unrelated object must be deleted.
            //var entry = _voteSystemDbContext.Entry(question);

            //if (entry.State != EntityState.Deleted)
            //{
            //    entry.State = EntityState.Deleted;
            //}
            //else
            //{
            //    _voteSystemDbContext.Set<Question>().Attach(question);

            //}

            _voteSystemDbContext.Set<Question>().Remove(question);
        }

        public IEnumerable<Question> GetAllQuestionsWithAnswers()
        {
            return _voteSystemDbContext
                                    .Questions
                                    .Include(x => x.Answers);
        }

        public IEnumerable<Question> GetUsersAnswersByVoteSystemId(Guid voteSystemId)
        {
            var result = _voteSystemDbContext.Questions
                                                .Where(x => x.VoteSystemId == voteSystemId)
                                                .Include(x => x.Answers.Select(y => y.ParticipantAnswers));

            return result;
        }
    }
}
