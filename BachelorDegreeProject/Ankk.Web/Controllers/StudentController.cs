namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using Ankk.Models;
    using Ankk.Web.Models;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    [Authorize]
    public class StudentController : BaseController
    {
        public StudentController(IAnkkData data)
            : base(data)
        {

        }

        // GET: Student
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var points = base.Data.Answers
                .All()
                .Where(p => p.UsersId == userId);

            var contests = base.Data.Contests
                                .All()
                                .Where(c => c.IsVisible == true);

            //HashSet<int> contestIds = new HashSet<int>();
            //List<int> results = new List<int>();

            //foreach (var point in points)
            //{
            //    var result = 0;
            //    var contestId = point.Questions.ContestId;
            //    foreach (var item in points)
            //    {
            //        if (!contestIds.Contains(contestId))
            //        {
            //            result += point.Points;
            //        }
            //    }

            //    contestIds.Add(contestId);
            //    results.Add(result);
            //}            

            ViewBag.Result = 17;

            var viewModel = Mapper.Map<IEnumerable<ContestViewModel>>(contests);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Task(int id)
        {
            var question = base.Data.Questions
                                .All()
                                .Where(t => t.ContestId == id);

            var viewModel = Mapper.Map<IList<QuestionViewModel>>(question);

            List<FileInfo> files = new List<FileInfo>();
            List<string> paths = new List<string>();

            foreach (var model in viewModel)
            {
                var dir = new DirectoryInfo(Server.MapPath("~/App_Data/Admin/" + model.ContestId + "/" + model.Name + "/description"));
                FileInfo[] fileNames = dir.GetFiles("*.*");

                var file = fileNames[0];
                files.Add(file);
                paths.Add(dir.ToString());
            }

            ViewBag.Files = files;
            ViewBag.Paths = paths;

            return this.View(viewModel);
        }

        public FileResult Download(string FileName, string FilePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath + "/" + FileName);
            string fileName = FileName;
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public ActionResult Solve(int id)
        {
            return this.View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Solve(HttpPostedFileBase file, AnswerViewModel model)
        {
            var question = this.Data
                            .Questions
                            .All()
                            .Where(q => q.Id == model.Id)
                            .FirstOrDefault();

            var contestId = question.ContestId;
            var questionName = question.Name;

            var contest = this.Data.
                                Contests
                                .All()
                                .Where(c => c.Id == contestId)
                                .FirstOrDefault();

            bool isCheckStrict = contest.IsStrict;

            var userEmail = User.Identity.Name;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                Directory.CreateDirectory(Server.MapPath("~/App_Data/" + userEmail + "/" + model.Id + "/uploads"));
                var path = Path.Combine(Server.MapPath("~/App_Data/" + userEmail + "/" + model.Id + "/uploads"), fileName);
                file.SaveAs(path);

                OpenCMD(model.Id, userEmail, contestId, questionName, isCheckStrict);
                int result = Compare(model.Id, userEmail, contestId, questionName) * 10;

                var userId = User.Identity.GetUserId();
                var answer = this.Data.Answers
                                    .All()
                                    .Where(a => a.QuestionId == model.Id && a.UsersId == userId)
                                    .FirstOrDefault();

                var currentAnswer = new Answer
                {
                    Points = result,
                    SourceCode = model.SourceCode,
                    QuestionId = model.Id,
                    UsersId = userId
                };

                if (answer != null)
                {
                    answer.Points = result;
                    answer.SourceCode = model.SourceCode;
                }
                else
                {
                    base.Data.Answers.Add(currentAnswer);
                }

                base.Data.SaveChanges();

                return this.RedirectToAction("View/" + model.Id, "Result");
            }

            return RedirectToAction("Index");
        }

        private int Compare(int id, string userEmail, int contestId, string questionName)
        {
            int counter = 0;
            for (int i = 1; i <= 10; i++)
            {
                string output = Path.Combine(Server.MapPath("~/App_Data/Admin/" + contestId + "/" + questionName + "/output"), "test." + i + ".out.txt");
                FileInfo outputFile = new FileInfo(output);

                string actual = Path.Combine(Server.MapPath("~/App_Data/" + userEmail + "/" + id + "/actualOutput"), "actualOutput" + i + ".txt");
                FileInfo actualFile = new FileInfo(actual);

                var areSameByHash = FilesAreEqual_Hash(outputFile, actualFile);

                if (areSameByHash)
                {
                    counter++;
                }
            }

            return counter;
        }

        private void OpenCMD(int id, string userEmail, int contestId, string questionName, bool isStrict)
        {
            var pathToUserUploadExe = Server.MapPath("~/App_Data/" + userEmail + "/" + id + "/uploads");
            var getFiles = Directory.GetFiles(pathToUserUploadExe);

            string pathToExe = Path.Combine(pathToUserUploadExe, getFiles[0]);

            Process cmd = new Process();

            ProcessStartInfo info = new ProcessStartInfo(pathToExe, "");
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            cmd.StartInfo = info;

            for (int i = 1; i <= 10; i++)
            {
                cmd.Start();
                string currentInput = "test." + i + ".in.txt";

                //@"D:\Ankk\Tests\Input\

                var pathInput = Path.Combine(Server.MapPath("~/App_Data/Admin/" + contestId + "/" + questionName + "/input"), currentInput);

                using (StreamReader reader = new StreamReader(pathInput))
                {
                    string line = reader.ReadLine();
                    cmd.StandardInput.WriteLine(line);

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        cmd.StandardInput.WriteLine(line);
                    }
                }

                String output = cmd.StandardOutput.ReadToEnd();

                string currentOutput = "actualOutput" + i + ".txt";

                //var pathToActualResult = Server.MapPath("~/App_Data/" + userEmail + "/" + id + "/actualOutput/" + currentOutput);

                //if (System.IO.File.Exists(pathToActualResult))
                //{
                //    System.IO.File.Delete(pathToActualResult);
                //}

                Directory.CreateDirectory(Server.MapPath("~/App_Data/" + userEmail + "/" + id + "/actualOutput"));
                var pathOutput = Path.Combine(Server.MapPath("~/App_Data/" + userEmail + "/" + id + "/actualOutput"), currentOutput);

                using (StreamWriter writer = new StreamWriter(pathOutput))
                {
                    if (!isStrict)
                    {
                        output = output.Trim();
                        output += Environment.NewLine;
                    }

                    writer.Write(output);
                }

                cmd.Close();
            }
        }

        private static bool FilesAreEqual_Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = null;
            byte[] secondHash = null;

            using (var writeFirstFile = first.OpenRead())
            {
                firstHash = MD5.Create().ComputeHash(writeFirstFile);
            }

            using (var writeSecondFile = second.OpenRead())
            {
                secondHash = MD5.Create().ComputeHash(writeSecondFile);
            }

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }

            return true;
        }
    }
}