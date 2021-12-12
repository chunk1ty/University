namespace MusicStore.Web.Models
{
    using System;

    using MusicStore.Models;

    public class SongDataModel
    {
        public static Func<Song, SongDataModel> FromDataToModel
        {
            get
            {
                return s => new SongDataModel()
                {
                    Id = s.SongId,
                    Title = s.Title,
                    Year = s.Year,
                    Genre = s.Genre,
                    AlbumId = s.AlbumId
                };
            }
        }

        public static Song FromModelToData(SongDataModel entity)
        {
            return new Song
            {
                Title = entity.Title,
                Year = entity.Year,
                Genre = entity.Genre,
                AlbumId = entity.AlbumId  
            };
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public int AlbumId { get; set; }
    }
}