using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Ankk.Models;
using Ankk.Web.Models;

namespace Ankk.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            Mapper.CreateMap<Contest, ContestViewModel>().ReverseMap();
            Mapper.CreateMap<Question, QuestionViewModel>().ReverseMap();
            Mapper.CreateMap<Answer, AnswerViewModel>().ReverseMap();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
