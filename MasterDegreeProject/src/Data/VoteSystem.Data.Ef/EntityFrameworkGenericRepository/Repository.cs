using System;
using System.Data.Entity;
using System.Linq;
using VoteSystem.Data.Ef.Contracts;

namespace VoteSystem.Data.Ef.EntityFrameworkGenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public Repository(IVoteSystemDbContext voteSystemDbContext)
        {
            if (voteSystemDbContext == null)
            {
                throw new ArgumentNullException(nameof(voteSystemDbContext));
            }

            VoteSystemDbContext = voteSystemDbContext;
            DbSet = VoteSystemDbContext.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; set; }
        protected IVoteSystemDbContext VoteSystemDbContext { get; set; }

        public virtual IQueryable<TEntity> All()
        {
            return Queryable.AsQueryable<TEntity>(DbSet);
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            var entry = VoteSystemDbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            var entry = VoteSystemDbContext.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            var entry = this.VoteSystemDbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
