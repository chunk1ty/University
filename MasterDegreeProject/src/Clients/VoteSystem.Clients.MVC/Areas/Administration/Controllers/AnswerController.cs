using System.Web.Mvc;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.Answer;
using VoteSystem.Clients.MVC.Infrastructure.Attributes;
using VoteSystem.Common.Constants;

namespace VoteSystem.Clients.MVC.Areas.Administration.Controllers
{
    public class AnswerController : AdminController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult AddNewAnswer(string containerPrefix)
        {
            ViewBag.ContainerPrefix = containerPrefix;

            return PartialView(PartialViewConstants.AnswerPartial, new AnswerViewModel());
        }
    }
}