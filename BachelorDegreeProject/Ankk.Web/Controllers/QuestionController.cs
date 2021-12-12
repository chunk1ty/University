namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using Ankk.Models;
    using Ankk.Web.Models;
    using AutoMapper;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    [Authorize(Roles = "Administrator")]
    public class QuestionController : BaseController
    {
        public QuestionController(IAnkkData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(int id, QuestionViewModel model, HttpPostedFileBase files)
        //{
        //    if (model != null && this.ModelState.IsValid)
        //    {                
        //        var baseModel = Mapper.Map<Question>(model);
        //        baseModel.ContestId = id;

        //        base.Data.Questions.Add(baseModel);
        //        base.Data.SaveChanges();

        //        return this.RedirectToAction("Index","Administration");
        //    }

        //    return this.View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HttpPostedFileBase description, IEnumerable<HttpPostedFileBase> input, IEnumerable<HttpPostedFileBase> output, QuestionViewModel model,int id)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var baseModel = new Question
                {
                    Name = model.Name,
                    ContestId = id
                };

                //Description
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/description"));
                var fileNamee = Path.GetFileName(description.FileName);
                var pathh = Path.Combine(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/description"), fileNamee);
                description.SaveAs(pathh);               

                //Input
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/input"));
                foreach (var file in input)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/input"), fileName);
                    file.SaveAs(path);
                }

                //Output
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/output"));

                foreach (var file in output)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Admin/" + id + "/" + model.Name + "/output"), fileName);
                    file.SaveAs(path);
                }

                base.Data.Questions.Add(baseModel);
                base.Data.SaveChanges();

                return this.RedirectToAction("Index", "Administration");
            }

            return this.View(model);
        }
    }
}