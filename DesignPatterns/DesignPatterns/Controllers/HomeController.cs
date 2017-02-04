using DesignPatterns.Data;
using DesignPatterns.Models;
using System;
using System.Web.Mvc;
using System.Linq;
using DesignPatterns.Patterns.Singlethon;
using DesignPatterns.Patterns.Adapter;
using DesignPatterns.Patterns.Factory;

namespace DesignPatterns.Controllers
{
    public class HomeController : Controller
    {
        private static Random random = new Random();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Distributor(int status)
        {
            IRedirect myRedirect = new RedirectFactory();

            return myRedirect.Redirect(status);
        }

        private void SeedData()
        {
            DesignPatternsDbContext context = new DesignPatternsDbContext();

            string[] goods = { "Kola", "Duvki", "Shokolad", "Fanta", "Banan" };
            string[] providers = { "Pesho OOD", "Gosho OOD", "Ivan OOD", "Spiridon OOD" };
            string[] receiver = { "Pesho", "Gosho", "Ivan", "Spiridon" };

            for (int i = 0; i < 10; i++)
            {
                var facture = new Facture()
                {
                    Date = DateTime.Now,
                    Goods = goods[GetRandomNumber(0, goods.Length)],
                    Price = 4.5M + i,
                    FactureNumber = GetRandomNumber(1000, 9999),
                    Provider = providers[GetRandomNumber(0, providers.Length)],
                    Receiver = receiver[GetRandomNumber(0, receiver.Length)]
                };
                context.Factures.Add(facture);
            }

            context.SaveChanges();
        }

        private int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max); ;
        }
    }
}