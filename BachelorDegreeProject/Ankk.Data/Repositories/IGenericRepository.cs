namespace Ankk.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All();

        IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions);

        void Add(T entity);

        void Delete(T entity);

        T Find(object id);

        T Delete(object id);

        void Update(T entity);

        void Detach(T entity);
    }
}
