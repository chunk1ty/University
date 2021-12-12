namespace MusicStore.Data
{
    using System.Data.Entity;

    using MusicStore.Data.Migrations;

    using MusicStore.Models;
    
    public class MusicStoreDbContext : DbContext, IMusicStoreDbContext
    {
        public MusicStoreDbContext()
            : base("MusicStoreConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicStoreDbContext, Configuration>());
        }

        public IDbSet<Song> Songs { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Artist> Artists { get; set; }


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