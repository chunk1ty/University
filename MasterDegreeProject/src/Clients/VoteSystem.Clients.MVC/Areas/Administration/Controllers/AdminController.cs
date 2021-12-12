using System.Web.Mvc;

using VoteSystem.Clients.MVC.Controllers;
using VoteSystem.Common.Constants;

namespace VoteSystem.Clients.MVC.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminController : BaseController
    {       
    }
}