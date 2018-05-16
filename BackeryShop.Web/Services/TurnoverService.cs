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



        public static List<TurnoverProductViewModel> GetDataForNewTurnover(Backery backery)
        {
            
            var result = new List<TurnoverProductViewModel>();
            var lastId = GetLastTurnoverId(backery);
            //var newData = GetNextTurnoverDataFromTurnoverId(backery, lastId);
            using (var db = new BackeryContext())
            {
                result = (from b in db.Backeries
                          join pl in db.PriceLists on b.PriceListId equals pl.Id
                          join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
                          join p in db.Products on pld.ProductId equals p.Id
                          where b.Id == backery.Id
                          orderby pld.OrderNo descending
                          select new TurnoverProductViewModel
                          {
                              DisplayName = p.Name,
                              ProdtId = p.Id,
                              Price = pld.Price
                          }).ToList();
            }

            var oldBalances = GetPreviousBackeryBalanceForTurnoverId(backery, lastId);
            //foreach (var item in result)
            //{
                
            //}
            return result;
        }



        public static List<TurnoverProductViewModel> GetPreviousBackeryBalanceForTurnoverId(Backery backery, int turnoverId)
        {
            var result = new List<TurnoverProductViewModel>();
            var prevData = GetPreviousTurnoverDataFromTurnoverId(backery, turnoverId);
            if (prevData != null)
            {
                using (var db = new BackeryContext())
                {
                    result = (from td in db.Turnovers
                              join tdd in db.TurnoverDetails on td.Id equals tdd.TurnoverId
                              where td.BackeryId == prevData.Id && td.ShiftNo == prevData.ShiftNo && td.Date.Equals(prevData.Date)
                              select new TurnoverProductViewModel
                              {
                                  ProdtId = tdd.ProductId,
                                  Balance = tdd.PreviousBalance
                              }
                   ).ToList();
                }
            }
            return result;
        }


        public static Turnover GetNextTurnoverDataForBakery(Backery backery)
        {
            var lastId = GetLastTurnoverId(backery);
            return GetNextTurnoverDataFromTurnoverId(backery, lastId);
        }

        public static int GetLastTurnoverId(Backery backery)
        {
            var id = 0;
            using (var db = new BackeryContext())
            {
                var t = db.Turnovers.Where(x => x.BackeryId == backery.Id);
                if (t.Any())
                {
                    id = t.Max(i => i.Id);
                }
                return id;
            };
        }

        public static Turnover GetNextTurnoverDataFromTurnoverId(Backery backery, int turnoverId)
        {
            var result = new Turnover
            {
                BackeryId = backery.Id,
                ShiftNo = 1,
                Date = DateTime.Today
            };
            using (var db = new BackeryContext())
            {
                var turnData = db.Turnovers.Find(turnoverId);
                if (turnData != null)
                {
                    result.BackeryId = backery.Id;
                    result.ShiftNo = (backery.NumberOfShifts == turnData.ShiftNo) ? 1 : turnData.ShiftNo + 1;
                    result.Date = (backery.NumberOfShifts == turnData.ShiftNo) ? turnData.Date.AddDays(1) : turnData.Date;
                }

            }
            return result;
        }

        public static Turnover GetPreviousTurnoverDataFromTurnoverId(Backery backery, int turnoverId)
        {
            using (var db = new BackeryContext())
            {
                var result = new Turnover();
                var turnData = db.Turnovers.Find(turnoverId);
                if (turnData == null) return null;
                result.BackeryId = backery.Id;
                result.ShiftNo = (turnData.ShiftNo == 1) ? backery.NumberOfShifts : turnData.ShiftNo - 1;
                result.Date = (turnData.ShiftNo == 1) ? turnData.Date.AddDays(-1) : turnData.Date;
                return result;
            }
        }
    }
}