using DesignPatterns.Controllers;
using System;
using System.Web.Mvc;

namespace DesignPatterns.Patterns.Factory
{
    public class RedirectFactory : Controller, IRedirect
    {
        public ActionResult Redirect(int status)
        {
            if (status == 1)
            {
                return this.RedirectToAction("Create", "Create");
            }
            else if (status == 2)
            {
                return this.RedirectToAction("Preview", "Preview");
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}