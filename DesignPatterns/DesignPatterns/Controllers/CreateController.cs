using DesignPatterns.Data;
using DesignPatterns.Models;
using System;
using System.Web.Mvc;
using System.Linq;
using DesignPatterns.Patterns.Singlethon;
using DesignPatterns.Patterns.Adapter;

namespace DesignPatterns.Controllers
{
    public class CreateController : Controller
    {
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(FactureViewModel model)
        {
            var factureDbModel = new Facture
            {
                Id = model.Id,
                Date = model.Date,
                FactureNumber = model.FactureNumber,
                Goods = model.Goods,
                Price = model.Price,
                Provider = model.Provider,
                Receiver = model.Receiver
            };

            Adapter adapter = new Adapter();
            adapter.SaveFacture(factureDbModel);

            return RedirectToAction("Index", "Home");
        }
    }
}