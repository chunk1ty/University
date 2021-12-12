namespace MusicStore.Web.Controllers
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using MusicStore.Data;
    using MusicStore.Web.Models;    

    public class ArtistsController : BaseApiController
    {
         public ArtistsController(IMusicStoreData data)
            : base (data)
        {
        }

        public ArtistsController()
            :base(new MusicStoreData())
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var artists = this.Data.Artists
                                .All()
                                .Select(ArtistDataModel.FromArtirst);

            return this.Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var artist = this.Data.Artists
                                .All()
                                .Where(a => a.ArtistId == id)
                                .Select(ArtistDataModel.FromArtirst)
                                .FirstOrDefault();

            if (artist == null)
            {
                return this.BadRequest("Invalid artist id");
            }

            return this.Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Post(ArtistDataModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newArtist = ArtistDataModel.FromModelToData(artist);

            this.Data.Artists.Add(newArtist);
            this.Data.SaveChanges();

            return this.Ok(newArtist);
        }

        [HttpPut]
        public IHttpActionResult Update(ArtistDataModel artist, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var artistToEdit = this.Data.Artists
                                    .All()
                                    .Where(a => a.ArtistId == id)
                                    .FirstOrDefault();

            if (artistToEdit == null)
            {
                return this.BadRequest("Invalid artist id");
            }

            artistToEdit.Name = artist.Name;
            artistToEdit.Country = artist.Country;
            artistToEdit.DateOfBirth = artist.DateOfBirth;

            this.Data.Artists.Update(artistToEdit);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artist = this.Data.Artists
                                    .All()
                                    .Where(a => a.ArtistId == id)
                                    .FirstOrDefault();

            if (artist == null)
            {
                return this.BadRequest("Invalid artist id");
            }

            this.Data.Artists.Delete(artist);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}
