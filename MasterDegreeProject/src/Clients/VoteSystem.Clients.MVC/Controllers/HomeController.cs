using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using VoteSystem.Clients.MVC.ViewModels.Home;

namespace VoteSystem.Clients.MVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IIdentityMessageService _identityMessageService;       

        public HomeController(IIdentityMessageService identityMessageService)
        {
            _identityMessageService = identityMessageService;            
        }

        [HttpGet]
        public ActionResult Index()
        {   
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(FeedbackViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            var message = new IdentityMessage()
            {
                Body = model.Body,
                Destination = model.Destination,
                Subject = model.Subject
            };

            await _identityMessageService.SendAsync(message);

            return this.RedirectToAction("Index");
        }
    }
}