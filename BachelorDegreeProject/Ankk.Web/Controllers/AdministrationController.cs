namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using Ankk.Models;
    using Ankk.Web.Models;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IAnkkData data)
            : base(data)
        {

        }
        
        public ActionResult Index()
        {
            var contests = base.Data.Contests.All();            
            var viewModel = Mapper.Map<IEnumerable<ContestViewModel>>(contests);

            return View(viewModel);
        }

        //ContestController
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContestViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var baseModel = Mapper.Map<Contest>(model);

                base.Data.Contests.Add(baseModel);
                base.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contest = base.Data.Contests
                .All()
                .Where(c => c.Id == id)                
                .FirstOrDefault();

            if (contest == null)
            {                
                return this.RedirectToAction("Index");
            }

            var viewModel = Mapper.Map<ContestViewModel>(contest);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContestViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {                
                var baseModel = Mapper.Map<Contest>(model);

                base.Data.Contests.Update(baseModel);
                base.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contest = base.Data.Contests
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (contest == null)
            {
                return HttpNotFound();
            }

            base.Data.Contests.Delete(contest);
            base.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {
            var users = base.Data.AppUsers
                            .All()
                            .Where(u => u.Roles.Count != 1);

            var usersToLIst = users.ToList();
            ViewBag.ContestID = id;

            return View(usersToLIst);
        }

        public ActionResult UserResult(string UserId, int ContestID)
        {
            var answers = this.Data.Answers
                        .All()
                        .Where(a => a.UsersId == UserId && a.Questions.ContestId == ContestID);

            var viewModel = Mapper.Map<IEnumerable<AnswerViewModel>>(answers);

            return this.View(viewModel);
        }
    }
}