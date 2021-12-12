namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using System.Web.Mvc;

    [Authorize]
    public class BaseController : Controller
    {
        // GET: Base
        protected IAnkkData Data { get; private set; }
        

        public BaseController(IAnkkData data)
        {
            this.Data = data;
        }
    }
}