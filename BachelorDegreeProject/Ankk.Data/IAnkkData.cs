namespace Ankk.Data
{
    using Ankk.Data.Repositories;
    using Ankk.Models;

    public interface IAnkkData
    {
       
        IGenericRepository<Score> Scores { get; }

        IGenericRepository<Contest> Contests { get; }

        IGenericRepository<Question> Questions { get; }

        IGenericRepository<Answer> Answers { get; }

        IGenericRepository<User> AppUsers { get; }

        void SaveChanges();
    }
}
