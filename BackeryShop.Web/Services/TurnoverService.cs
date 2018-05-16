using BackeryShop.Web.Models.ViewModels;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackeryShop.Web.Services
{
    public static class TurnoverService
    {
        public static List<TurnoverProductViewModel> GetTurnoversForBackery(Backery backery)
        {
            return GetTurnoversForBackeryId(backery.Id);
        }

        public static List<TurnoverProductViewModel> GetTurnoversForBackeryId(int id)
        {
            var result = new List<TurnoverProductViewModel>();
            using (var db = new BackeryContext())
            {
                result = (from b in db.Backeries
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
            }
            return result;
        }
    }
}