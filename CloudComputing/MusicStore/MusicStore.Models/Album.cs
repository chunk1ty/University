namespace MusicStore.Models
{
    using System;
    using System.Collections.Generic;

    public class Album
    {
        public Album()
        {
            this.Artists = new HashSet<Artist>();
            this.Songs = new HashSet<Song>();
        }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Producer { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
