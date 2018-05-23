using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BackeryShopDomain.Classes;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShopDomain.DataModel.Repositories
{
    public static class TurnoverRepository
    {

        public static List<TurnoverDetailDto> GetDataForNewTurnover(Backery backery)
        {
            var result = new List<TurnoverDetailDto>();
            var lastId = TurnoverRepository.GetLastTurnoverId(backery);
            using (var db = new BackeryContext())
            {
                result = (from b in db.Backeries
                    join pl in db.PriceLists on b.PriceListId equals pl.Id
                    join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
                    join p in db.Products on pld.ProductId equals p.Id
                    where b.Id == backery.Id
                    orderby pld.OrderNo descending
                    select new TurnoverDetailDto
                    {
                        ProductName = p.Name,
                        ProductId = p.Id,
                        Price = pld.Price
                    }).ToList();
            }
            var oldBalances = GetBalancesForTurnoverId(backery.Id, lastId);
            if (oldBalances.Any())
            {
                foreach (var item in result)
                {
                    var oldBal = oldBalances.Where(x => x.ProductId == item.ProductId).ToList();
                    if (oldBal.Any())
                    {
                        item.PreviousBalance = oldBal.First().PreviousBalance;
                    }
                }
            }
            return result;
        }

        public static List<TurnoverDetailDto> GetDataForNewTurnover(int backeryId, DateTime date, int shift)
        {
            var result = new List<TurnoverDetailDto>();
            
            using (var db = new BackeryContext())
            {
                result = (from b in db.Backeries
                    join pl in db.PriceLists on b.PriceListId equals pl.Id
                    join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
                    join p in db.Products on pld.ProductId equals p.Id
                    where b.Id == backeryId
                          orderby pld.OrderNo descending
                    select new TurnoverDetailDto
                    {
                        ProductName = p.Name,
                        ProductId = p.Id,
                        Price = pld.Price
                    }).ToList();



            }

            var lastId = 0;
            var oldBalances = GetBalancesForTurnoverId(backeryId, lastId);
            if (oldBalances.Any())
            {
                foreach (var item in result)
                {
                    var oldBal = oldBalances.Where(x => x.ProductId == item.ProductId).ToList();
                    if (oldBal.Any())
                    {
                        item.PreviousBalance = oldBal.First().PreviousBalance;
                    }
                }
            }
            return result;
        }


        public static List<TurnoverDetailDto> GetBalancesForTurnoverId(int backeryId, int turnoverId)
        {
            var result = new List<TurnoverDetailDto>();
            using (var db = new BackeryContext())
            {
                result = (from td in db.Turnovers
                        join tdd in db.TurnoverDetails on td.Id equals tdd.TurnoverId
                        where td.BackeryId == backeryId && td.Id == turnoverId
                        select new TurnoverDetailDto
                        {
                            ProductId = tdd.ProductId,
                            PreviousBalance = tdd.PreviousBalance
                        }
                    ).ToList();
            }
            return result;
        }

        public static int SaveTurnoverData(TurnoverDto dataDto)
        {
            var result = -1;
            if (dataDto != null)
            {

                using (var db = new BackeryContext())
                {
                    var turnover = new Turnover
                    {
                        Date = dataDto.Date,
                        BackeryId = dataDto.BackeryId,
                        ShiftNo = dataDto.ShiftNo
                    };
                    db.Turnovers.Add(turnover);
                    db.SaveChanges();
                    var lastId = turnover.Id;
                    var tDetails = new List<TurnoverDetail>();
                    foreach (var item in dataDto.TurnoverDetails)
                    {
                        var td = new TurnoverDetail
                        {
                            ProductId = item.ProductId,
                            Price = item.Price,
                            PreviousBalance = item.PreviousBalance,
                            BakedNew = item.BakedNew,
                            Sold = item.Sold,
                            Scrap = item.Scrap,
                            NewBalance = item.NewBalance,
                            TurnoverId = lastId
                        };
                        tDetails.Add(td);
                        
                    };
                    db.TurnoverDetails.AddRange(tDetails);
                    db.SaveChanges();
                }
                return 1;

            }
            return result;
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

        public static int GetTurnoverIdFromDataAndShift(Backery backery, DateTime date, int shift)
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