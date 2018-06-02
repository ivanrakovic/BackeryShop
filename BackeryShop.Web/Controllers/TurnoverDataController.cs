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
using System.IO;

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
            model.ProductList = new TurnoverProductViewModel
            {
                LastTurnoverId = newDataForBakery.LastTurnoverId,
                IsEditMode = false,
                ProductsData = TurnoverRepository.GetDataForNewTurnover(id)                
            };

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult InsertDataTurnover(TurnoverDto turnover)
        {

            if (ModelState.IsValid)
            {
                var i = TurnoverRepository.SaveTurnoverData(turnover);
                return Json(new { success = true });
            }

            var model = new TurnoverViewModel();
            return View("Create", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateDataTurnover(TurnoverDto turnover)
        {

            if (ModelState.IsValid)
            {
                var i = TurnoverRepository.UpdateTurnoverData(turnover);
                return Json(new { success = true });
            }

            var model = new TurnoverViewModel();
            return View("Create", model);
        }

        [HttpGet]
        public ActionResult GetTurnoverDataForDateShift(int backeryId, DateTime date, int shift)
        {
            var model = new TurnoverProductViewModel();
            if (ModelState.IsValid)
            {
                var turnover = TurnoverRepository.GetDataForTurnoverFromDataAndShift(backeryId, date, shift);
                model.LastTurnoverId = turnover.LastTurnoverId;
                model.IsEditMode = turnover.IsExistingTurnover;
                model.ProductsData = turnover.TurnoverDetails;
                model.TurnoverId = turnover.Id;
            }
            return PartialView("TurnoverDetails", model);
        }


        public static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}