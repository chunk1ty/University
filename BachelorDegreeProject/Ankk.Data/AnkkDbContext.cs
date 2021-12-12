namespace Ankk.Data
{
    using Ankk.Data.Migrations;
    using Ankk.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class AnkkDbContext : IdentityDbContext<User>, IAnkkDbContext
    {

        public AnkkDbContext()
            : base("AnkkConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AnkkDbContext, Configuration>());
        }

        public static AnkkDbContext Create()
        {
            return new AnkkDbContext();
        }


       
        public IDbSet<Score> Scores { get; set; }

        public IDbSet<Contest> Contests { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {            
            base.SaveChanges();
        }
    }
}
