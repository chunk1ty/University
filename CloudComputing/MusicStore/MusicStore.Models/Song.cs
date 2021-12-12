namespace MusicStore.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
