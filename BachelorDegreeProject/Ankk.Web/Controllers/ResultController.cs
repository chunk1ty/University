namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using Ankk.Models;
    using Ankk.Web.Models;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class ResultController : BaseController
    {
        public ResultController(IAnkkData data)
            : base(data)
        {

        }

        public ActionResult View(int id)
        {
            var userId = User.Identity.GetUserId();

            var results = this.Data.Answers
                .All()
                .Where(r => r.QuestionId == id && r.UsersId == userId).FirstOrDefault();

            if (results == null)
            {
                return this.RedirectToAction("ViewIsEmpty");
            }

            var viewModel = Mapper.Map<AnswerViewModel>(results);

            return View(viewModel);
        }

        public ActionResult ViewIsEmpty()
        {
            return this.View();
        }
    }
}