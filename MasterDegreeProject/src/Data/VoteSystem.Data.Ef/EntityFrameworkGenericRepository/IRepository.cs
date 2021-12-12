using System.Linq;

namespace VoteSystem.Data.Ef.EntityFrameworkGenericRepository
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);

        IQueryable<T> All();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
