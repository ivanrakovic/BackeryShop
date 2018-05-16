using System.Linq;
using System.Web.Mvc;
using BackeryShop.Web.Models.ViewModels;
using BackeryShop.Web.Services;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;

namespace BackeryShop.Web.Controllers
{
    public class TurnoverDataController : Controller
    {
        // GET: TurnoverData
        public ActionResult Create(int id)
        {
            var model = new TurnoverViewModel();

            using (var db = new BackeryContext())
            {
                var backery = db.Backeries.Find(id);
                model.Backery = backery;
            };

            var newDataForBakery = TurnoverService.GetNextTurnoverDataForBakery(model.Backery);
            model.Shift = newDataForBakery.ShiftNo;
            model.Date = newDataForBakery.Date;
            model.TurnoverProductsViewModels = TurnoverService.GetDataForNewTurnover(model.Backery);

            return View(model);
        }


    }
}