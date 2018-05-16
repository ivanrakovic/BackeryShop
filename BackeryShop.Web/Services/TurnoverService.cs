using BackeryShop.Web.Models.ViewModels;
using BackeryShopDomain.Classes;
using BackeryShopDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace BackeryShop.Web.Services
{
    public static class TurnoverService
    {
        public static List<TurnoverProductViewModel> GetDataForNewTurnover(Backery backery)
        {
            var result = new List<TurnoverProductViewModel>();
            var lastId = GetLastTurnoverId(backery);
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
            var oldBalances = GetBalancesForTurnoverId(backery, lastId);
            if (oldBalances.Any())
            {
                foreach (var item in result)
                {
                    var oldBal = oldBalances.Where(x => x.ProdtId == item.ProdtId);
                    if (oldBal.Any())
                    {
                        item.Balance = oldBal.First().Balance;
                    }                    
                }
            }
            return result;
        }

        public static List<TurnoverProductViewModel> GetDataForTurnover(Backery backery, DateTime date, int shift)
        {
            var result = new List<TurnoverProductViewModel>();
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

           // GetTurnoverIdFromDataAndShift

            return result;
        }

        public static List<TurnoverProductViewModel> GetBalancesForTurnoverId(Backery backery, int turnoverId)
        {
            var result = new List<TurnoverProductViewModel>();
            using (var db = new BackeryContext())
            {
                result = (from td in db.Turnovers
                          join tdd in db.TurnoverDetails on td.Id equals tdd.TurnoverId
                          where td.BackeryId == backery.Id && td.Id == turnoverId
                          select new TurnoverProductViewModel
                          {
                              ProdtId = tdd.ProductId,
                              Balance = tdd.PreviousBalance
                          }
                    ).ToList();
            }
            return result;
        }

        public static Turnover GetNextTurnoverDataForBakery(Backery backery)
        {
            var lastId = GetLastTurnoverId(backery);
            return GetNextTurnoverDataFromTurnoverId(backery, lastId);
        }

        private static int GetLastTurnoverId(Backery backery)
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

        private static Turnover GetPreviousTurnoverDataFromTurnoverId(Backery backery, int turnoverId)
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

        private static int GetTurnoverIdFromDataAndShift(Backery backery, DateTime date, int shift)
        {
            var id = 0;
            using (var db = new BackeryContext())
            {
                var t = db.Turnovers.Where(x => x.Date == date && x.BackeryId == backery.Id && x.ShiftNo == shift);
                if (t.Any())
                {
                    id = t.Max(i => i.Id);
                }
                return id;
            };
        }
    }
}