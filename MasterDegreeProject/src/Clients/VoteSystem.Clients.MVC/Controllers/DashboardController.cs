using System;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using VoteSystem.Clients.MVC.Areas.Administration.ViewModels.VoteSystem;
using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Data.Services.Contracts;

namespace VoteSystem.Clients.MVC.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IVoteSystemService _voteSystemService;

        public DashboardController(IVoteSystemService voteSystemService)
        {
            _voteSystemService = voteSystemService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var voteSystemUserId = new Guid(User.Identity.GetUserId());

            var voteSystems = _voteSystemService
                            .GetAllAvailableVoteSystemsForUserByUserId(voteSystemUserId)
                            .To<VoteSystemViewModel>()
                            .ToList();

            return View(voteSystems);
        }
    }
}