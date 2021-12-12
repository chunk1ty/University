namespace MusicStore.Web.Controllers
{
    using MusicStore.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        private IMusicStoreData db;

        public BaseApiController(IMusicStoreData db)
        {
            this.db = db;
        }

        protected IMusicStoreData Data 
        { 
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
            }
        }
    }
}
