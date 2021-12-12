using System.Data.Entity;
using VoteSystem.Data.Ef;
using VoteSystem.Data.Ef.Migrations;

namespace VoteSystem.Clients.MVC
{
    public class DbConfig
    {
        public static void RegisterDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoteSystemDbContext, Configuration>());
        }
    }
}