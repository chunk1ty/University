namespace MusicStore.Web.Controllers
{
    using MusicStore.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using MusicStore.Web.Models;

    public class SongsController : BaseApiController
    {
        public SongsController(IMusicStoreData data)
            : base (data)
        {
        }

        public SongsController()
            :base(new MusicStoreData())
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var songs = this.Data.Songs
                            .All()
                            .Select(SongDataModel.FromDataToModel);

            return this.Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var song = this.Data.Songs
                                .SearchFor(s => s.SongId == id)
                                .Select(SongDataModel.FromDataToModel)
                                .FirstOrDefault();

            if (song == null)
            {
                return this.BadRequest("Invalid song id");
            }

            return this.Ok(song);
        }

        [HttpPost]
        public IHttpActionResult Post(SongDataModel song)
        {
            if (!this.ModelState.IsValid)
	        {
		        return this.BadRequest(this.ModelState);
	        }

            var newSong = SongDataModel.FromModelToData(song);

            this.Data.Songs.Add(newSong);
            this.Data.SaveChanges();

            return this.Ok(newSong);
        }

        [HttpPut]
        public IHttpActionResult Update(SongDataModel song, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var songToEdit = this.Data.Songs
                                    .SearchFor(s => s.SongId == id)
                                    .FirstOrDefault();

            if (songToEdit == null)
            {
                return this.BadRequest("Invalid song id");
            }

            songToEdit.Title = song.Title;
            songToEdit.Year = song.Year;
            songToEdit.Genre = song.Genre;

            this.Data.Songs.Update(songToEdit);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var song = this.Data.Songs
                               .SearchFor(s => s.SongId == id)
                               .FirstOrDefault();

            if (song == null)
            {
                return this.BadRequest("Invalid song id");
            }

            this.Data.Songs.Delete(song);
            this.Data.SaveChanges();

            return this.Ok(song);
        }
    }
}