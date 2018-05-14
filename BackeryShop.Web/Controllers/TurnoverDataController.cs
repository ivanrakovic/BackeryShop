using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackeryShop.Web.Models.ViewModels;
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
                model.TurnoverProductsViewModels = (from b in db.Backeries
                    join pl in db.PriceLists on b.PriceListId equals pl.Id
                    join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
                    join p in db.Products on pld.ProductId equals p.Id
                    where b.Id == id
                    select new TurnoverProductViewModel
                    {
                        DisplayName = p.Name,
                        ProdtId = p.Id,
                        Price = pld.Price
                    }).ToList();
            };
            return View(model);
        }
    }
}