using System;
using System.Data.Entity;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;

using VoteSystem.Common.Constants;
using VoteSystem.Data.Contracts;
using VoteSystem.Data.Ef.Contracts;
using VoteSystem.Data.Ef.Models;
using VoteSystem.Data.Entities;
using VoteSystem.Data.Entities.Contracts;

namespace VoteSystem.Data.Ef
{
    public class VoteSystemDbContext : IdentityDbContext<AspNetUser>, IVoteSystemDbContext, IVoteSystemEfDbContextSaveChanges
    {
        public VoteSystemDbContext()
            : base(ConnectionStings.VoteSystemDbConnection, throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Entities.VoteSystem> VoteSystems { get; set; }

        public virtual IDbSet<Participant> Participants { get; set; }

        public virtual IDbSet<ParticipantAnswer> ParticipantAnswers { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Answer> QuestionAnswers { get; set; }

        public virtual IDbSet<VoteSystemUser> VoteSystemUsers { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() 
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public static VoteSystemDbContext Create()
        {
            return new VoteSystemDbContext();
        }

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();

            return base.SaveChanges();
        }

        public void SetDataBaseTimeout(TimeSpan time)
        {
            Database.CommandTimeout = (int)time.TotalSeconds;
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            // ChangeTracker get all changes before they go to the Database
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
