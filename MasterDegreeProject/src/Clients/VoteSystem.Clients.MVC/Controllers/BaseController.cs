using System.Web.Mvc;

using AutoMapper;

using Ninject;
using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Services.Web.Contracts;

namespace VoteSystem.Clients.MVC.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        [Inject]
        public ICacheService Cache { get; set; }

        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();
    }
}