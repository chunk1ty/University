
namespace MusicStore.Data
{
    using MusicStore.Data.Repository;
    using MusicStore.Models;

    public interface IMusicStoreData
    {
        IGenericRepository<Song> Songs { get; }

        IGenericRepository<Album> Albums { get; }

        IGenericRepository<Artist> Artists { get; }

        void SaveChanges();
    }
}
