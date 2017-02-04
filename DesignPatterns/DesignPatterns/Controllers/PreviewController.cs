namespace DesignPatterns.Controllers
{
    using DesignPatterns.Models;
    using DesignPatterns.Patterns.Singlethon;
    using System.Linq;
    using System.Web.Mvc;

    public class PreviewController : Controller
    {
        public ActionResult Preview()
        {
            var factures = DdAcessSinglethon.Instance.GetContext()
                            .Factures
                            .Select(x => new FactureViewModel()
                            {
                                Id = x.Id,
                                Date = x.Date,
                                FactureNumber = x.FactureNumber,
                                Goods = x.Goods,
                                Price = x.Price,
                                Provider = x.Provider,
                                Receiver = x.Receiver
                            })
                            .ToList();

            return View(factures);
        }
    }
}