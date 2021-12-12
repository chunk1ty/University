namespace MusicStore.Web.Models
{
    using MusicStore.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ArtistDataModel
    {
        public static Expression<Func<Artist, ArtistDataModel>> FromArtirst
        {
            get
            {
                return a => new ArtistDataModel()
                {
                   Id = a.ArtistId,
                   Name = a.Name,
                   Country = a.Country,
                   DateOfBirth = a.DateOfBirth
                };
            }
        }


        public static Artist FromModelToData(ArtistDataModel entity)
        {
            return new Artist
            {
                Name = entity.Name,
                Country = entity.Country,
                DateOfBirth = entity.DateOfBirth
            };
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}