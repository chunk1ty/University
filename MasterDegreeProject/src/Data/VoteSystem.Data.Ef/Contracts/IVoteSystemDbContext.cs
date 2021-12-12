using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Ef.Contracts
{
    public interface IVoteSystemDbContext
    {
        IDbSet<VoteSystemUser> VoteSystemUsers { get; set; }

        IDbSet<Participant> Participants { get; set; }

        IDbSet<ParticipantAnswer> ParticipantAnswers { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<Entities.VoteSystem> VoteSystems { get; set; }

        IDbSet<Answer> QuestionAnswers { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SetDataBaseTimeout(TimeSpan time);
    }
}