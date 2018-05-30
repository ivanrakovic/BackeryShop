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
            model.LastTurnoverId = newDataForBakery.LastTurnoverId;
            model.IsEditMode = false;
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
                return Json(new { success = true });
            }

            var model = new TurnoverViewModel();
            return View("Create", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateTurnover(TurnoverDto turnover)
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
        public JsonResult GetTurnoverDataForDateShift(int backeryId, DateTime date, int shift)
        {
            var model = new List<TurnoverDetailDto>();
            if (ModelState.IsValid)
            {
                var modelData = TurnoverRepository.GetDataForTurnoverFromDataAndShift(backeryId, date, shift);
                return Json(new
                {
                    view = RenderRazorViewToString(ControllerContext, "TurnoverDetails", modelData.TurnoverDetails.ToList()),
                    lastTurnoverId = modelData.LastTurnoverId
                },JsonRequestBehavior.AllowGet);
            }
            return null;//View("Create", model);
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