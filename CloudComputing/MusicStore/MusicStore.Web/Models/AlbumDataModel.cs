namespace MusicStore.Web.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MusicStore.Models;
    using System.Linq.Expressions;

    public class AlbumDataModel
    {       
        public static Expression<Func<Album, AlbumDataModel>> FromAlbum
        {
            get
            {
                return a => new AlbumDataModel()
                {
                    Id = a.AlbumId,
                    Title = a.Title,
                    Year = a.Year,
                    Producer = a.Producer
                };
            }
        }

        public static Album FromModelToData(AlbumDataModel entity)
        {
            return new Album
            {
                Title = entity.Title,
                Year = entity.Year,
                Producer = entity.Producer
            };
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Producer { get; set; }
    }
}