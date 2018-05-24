using BackeryShop.Web.Models.ViewModels;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using BackeryShopDomain.DataModel.Repositories;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShop.Web.Services
{
    public static class TurnoverService
    {
        public static List<TurnoverDetailDto> GetDataForTurnover(int backeryId, DateTime date, int shiftNo)
        {
            var result = new List<TurnoverDetailDto>();
            return result;
        }

        //public static List<TurnoverDetailDto> GetDataForTurnover(Backery backery, DateTime date, int shift)
        //{
        //    var result = new List<TurnoverDetailDto>();
        //    using (var db = new BackeryContext())
        //    {
        //        result = (from b in db.Backeries
        //                  join pl in db.PriceLists on b.PriceListId equals pl.Id
        //                  join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
        //                  join p in db.Products on pld.ProductId equals p.Id
        //                  where b.Id == backery.Id
        //                  orderby pld.OrderNo descending
        //                  select new TurnoverDetailDto
        //                  {
        //                      ProductName = p.Name,
        //                      ProductId = p.Id,
        //                      Price = pld.Price
        //                  }).ToList();
        //    }

        //    // GetTurnoverIdFromDataAndShift

        //    return result;
        //}

        //public static List<TurnoverDetailDto> GetBalancesForTurnoverId(int backeryId, int turnoverId)
        //{
        //    var result = new List<TurnoverDetailDto>();
        //    using (var db = new BackeryContext())
        //    {
        //        result = (from td in db.Turnovers
        //                  join tdd in db.TurnoverDetails on td.Id equals tdd.TurnoverId
        //                  where td.BackeryId == backeryId && td.Id == turnoverId
        //                  select new TurnoverDetailDto
        //                  {
        //                      ProductId = tdd.ProductId,
        //                      PreviousBalance = tdd.PreviousBalance
        //                  }
        //            ).ToList();
        //    }
        //    return result;
        //}

        public static TurnoverDto GetNextTurnoverDataForBakery(int backeryId)
        {
            var lastId = TurnoverRepository.GetLastTurnoverId(backeryId);
            return TurnoverRepository.GetNextTurnoverDataFromTurnoverId(backeryId, lastId);
        }


        //private static Turnover GetPreviousTurnoverDataFromTurnoverId(Backery backery, int turnoverId)
        //{
        //    using (var db = new BackeryContext())
        //    {
        //        var result = new Turnover();
        //        var turnData = db.Turnovers.Find(turnoverId);
        //        if (turnData == null) return null;
        //        result.BackeryId = backery.Id;
        //        result.ShiftNo = (turnData.ShiftNo == 1) ? backery.NumberOfShifts : turnData.ShiftNo - 1;
        //        result.Date = (turnData.ShiftNo == 1) ? turnData.Date.AddDays(-1) : turnData.Date;
        //        return result;
        //    }
        //}
    }
}