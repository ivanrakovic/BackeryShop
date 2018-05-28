using System;
using System.Collections.Generic;
using System.Linq;
using BackeryShopDomain.Classes;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShopDomain.DataModel.Repositories
{
    public static class TurnoverRepository
    {

        public static List<TurnoverDetailDto> GetDataForNewTurnover(int backeryId)
        {
            var result = new List<TurnoverDetailDto>();
            var lastId = GetLastTurnoverId(backeryId);
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
            var oldBalances = GetBalancesForTurnoverId(backeryId, lastId);
            if (oldBalances.Any())
            {
                foreach (var item in result)
                {
                    var oldBal = oldBalances.Where(x => x.ProductId == item.ProductId).ToList();
                    if (oldBal.Any())
                    {
                        item.PreviousBalance = oldBal.First().NewBalance;
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


        public static List<TurnoverDetailDto> GetDataForTurnoverFromDataAndShift(int backeryId, DateTime date, int shift)
        {
            var result = new List<TurnoverDetailDto>();
            var targetId = GetTurnoverIdFromDataAndShift(backeryId, date, shift);
            
            
            var oldBalances = GetBalancesForTurnoverId(backeryId, targetId);
            if (oldBalances.Any())
            {
                result = oldBalances;
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
                              ProductName = tdd.ProductName,
                              NewBalance = tdd.NewBalance,
                              Scrap = tdd.Scrap,
                              Sold = tdd.Sold,
                              Price = tdd.Price,
                              BakedNew = tdd.BakedNew,
                              PreviousBalance = tdd.PreviousBalance
                          }
                    ).ToList();
            }
            return result;
        }

        public static TurnoverDto GetNextTurnoverDataFromTurnoverId(int backeryId, int turnoverId)
        {
            var result = new TurnoverDto
            {
                BackeryId = backeryId,
                ShiftNo = 1,
                Date = DateTime.Today
            };
            using (var db = new BackeryContext())
            {
                var backery = db.Backeries.Find(backeryId);
                var turnData = db.Turnovers.Find(turnoverId);
                if (turnData != null)
                {
                    result.BackeryId = backery.Id;
                    result.ShiftNo = (backery.NumberOfShifts == turnData.ShiftNo) ? 1 : turnData.ShiftNo + 1;
                    result.Date = (backery.NumberOfShifts == turnData.ShiftNo) ? turnData.Date.AddDays(1) : turnData.Date;
                    result.LastTurnoverId = turnoverId;
                }
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
                        ShiftNo = dataDto.ShiftNo,
                        LastTurnoverId = dataDto.LastTurnoverId,
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
                            ProductName = item.ProductName,
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
        public static int GetLastTurnoverId(int backeryId)
        {
            var id = 0;
            using (var db = new BackeryContext())
            {
                var t = db.Turnovers.Where(x => x.BackeryId == backeryId);
                if (t.Any())
                {
                    id = t.Max(i => i.Id);
                }
                return id;
            };
        }

        public static int GetTurnoverIdFromDataAndShift(int backeryId, DateTime date, int shift)
        {
            var id = 0;
            using (var db = new BackeryContext())
            {
                var t = db.Turnovers.Where(x => x.Date == date && x.BackeryId == backeryId && x.ShiftNo == shift);
                if (t.Any())
                {
                    id = t.Max(i => i.Id);
                }
                return id;
            };
        }

        public static BackeryDto GetBackery(int backeryId)
        {
            var result = new BackeryDto();
            using (var db = new BackeryContext())
            {
                var backery = db.Backeries.Find(backeryId);
                if (backery != null)
                {
                    result.Id = backery.Id;
                    result.Name = backery.Name;
                    result.NumberOfShifts = backery.NumberOfShifts;
                }
            };
            return result;
        }
        public static List<BackeryDto> GetAllBackeries()
        {
            var result = new List<BackeryDto>();
            using (var db = new BackeryContext())
            {
                var backeriesList = db.Backeries.ToList();
                if (backeriesList.Any())
                {
                    foreach (var item in backeriesList)
                    {
                        var backery = new BackeryDto
                        {
                            Id = item.Id,
                            Name = item.Name,
                            NumberOfShifts = item.NumberOfShifts
                        };
                        result.Add(backery);
                    }
                }
            }
            return result;
        }

    }
}