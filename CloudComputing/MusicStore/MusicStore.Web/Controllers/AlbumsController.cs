namespace MusicStore.Web.Controllers
{
    using System.Linq;
    using MusicStore.Data;
    using System.Web.Http;
    using MusicStore.Web.Models;
    using MusicStore.Models;

    public class AlbumsController : BaseApiController
    {
        public AlbumsController(IMusicStoreData data)
            : base(data)
        {
        }

        public AlbumsController()
            : base(new MusicStoreData())
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var albums = this.Data.Albums
                                .All()
                                .Select(AlbumDataModel.FromAlbum);

            return this.Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var album = this.Data.Albums
                                .All()
                                .Where(a => a.AlbumId == id)
                                .Select(AlbumDataModel.FromAlbum)
                                .FirstOrDefault();

            if (album == null)
            {
                return BadRequest("Album does not exist - invalid id");
            }

            return this.Ok(album);
        }

        [HttpPost]
        public IHttpActionResult Post(AlbumDataModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var newAlbum = AlbumDataModel.FromModelToData(album);

            this.Data.Albums.Add(newAlbum);
            this.Data.SaveChanges();

            return this.Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(AlbumDataModel album, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var albumToUpdate = this.Data.Albums
                                        .All()
                                        .Where(a => a.AlbumId == id)
                                        .FirstOrDefault();

            if (albumToUpdate == null)
            {
                return this.BadRequest("Invalid album id");
            }

            albumToUpdate.Title = album.Title;
            albumToUpdate.Year = album.Year;
            albumToUpdate.Producer = album.Producer;

            this.Data.Albums.Update(albumToUpdate);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var album = this.Data.Albums
                                .All()
                                .Where(a => a.AlbumId == id)
                                .FirstOrDefault();

            if (album == null)
            {
                return this.BadRequest("Invalid album id");
            }

            this.Data.Albums.Delete(album);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}
