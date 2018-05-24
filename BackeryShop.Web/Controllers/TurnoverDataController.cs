using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BackeryShop.Web.Models.ViewModels;
using BackeryShop.Web.Services;
using BackeryShopDomain.Classes;
using BackeryShopDomain.Classes.Entities;
using BackeryShopDomain.DataModel;
using Microsoft.SqlServer.Server;
using BackeryShopDomain.DataModel.Repositories;

namespace BackeryShop.Web.Controllers
{
    public class TurnoverDataController : Controller
    {
        // GET: TurnoverData
        public ActionResult Create(int id)
        {
            var model = new TurnoverViewModel
            {
                Backery = TurnoverRepository.GetBackery(id),                
            };

            var newDataForBakery = TurnoverService.GetNextTurnoverDataForBakery(id);
            model.Shift = newDataForBakery.ShiftNo;
            model.Date = newDataForBakery.Date;
            model.TurnoverProductsViewModels = TurnoverRepository.GetDataForNewTurnover(id);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult InsertTurnover(TurnoverDto turnover)
        {
            
            if (ModelState.IsValid)
            {
                var i = TurnoverRepository.SaveTurnoverData(turnover);
                return Json(new {success = true});
            }

            var model = new TurnoverViewModel();
            return View("Create",model);
        }

    }
}