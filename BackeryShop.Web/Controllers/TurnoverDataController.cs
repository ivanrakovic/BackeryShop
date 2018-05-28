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
using System.Collections.Generic;
using System;

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
            model.LastTurnoverId = newDataForBakery.LastTurnoverId;
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

        [HttpGet]
        public ActionResult GetTurnoverDataForDateShift(int backeryId, DateTime date, int shift)
        {
            var model = new List<TurnoverDetailDto>();
            if (ModelState.IsValid)
            {
                var modelData = TurnoverRepository.GetDataForTurnoverFromDataAndShift(backeryId, date, shift);
                return PartialView("TurnoverDetails", modelData);
            }


            return null;//View("Create", model);
        }
    }
}