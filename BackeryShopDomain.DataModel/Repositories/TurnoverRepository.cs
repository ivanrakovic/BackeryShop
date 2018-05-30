using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BackeryShopDomain.Classes;
using BackeryShopDomain.Classes.Entities;
using BackeryShopDomain.DataModel.Helpers;

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

        public static TurnoverDto GetDataForNewTurnover(int backeryId, DateTime date, int shift)
        {
            var result = new TurnoverDto
            {
                BackeryId = backeryId,
                Date = date,
                ShiftNo = shift
            };


            using (var db = new BackeryContext())
            {
                result.TurnoverDetails = (from b in db.Backeries
                          join pl in db.PriceLists on b.PriceListId equals pl.Id
                          join pld in db.PriceListDetails on pl.Id equals pld.PriceListId
                          join p in db.Products on pld.ProductId equals p.Id
                          where b.Id == backeryId
                          orderby pld.OrderNo descending
                          select new TurnoverDetailDto
                          {
                              ProductName = p.Name,
                              ProductId = p.Id,
                              Price = pld.Price,
                              PreviousBalance = 0,
                              BakedNew = 0,
                              NewBalance = 0,
                              Scrap = 0,
                              Sold = 0
                          }).ToList();

            }

            //var lastId = 0;
            var prevBalances = GetPreviousTurnoverData(backeryId, date, shift);
            var nextBalances = GetNextTurnoverData(backeryId, date, shift);
            if (prevBalances != null && prevBalances.TurnoverDetails != null && prevBalances.TurnoverDetails.Any())
            {
                result.LastTurnoverId = prevBalances.Id;
                foreach (var item in result.TurnoverDetails)
                {
                    var oldBal = prevBalances.TurnoverDetails.Where(x => x.ProductId == item.ProductId).ToList();
                    if (oldBal.Any())
                    {
                        item.PreviousBalance = oldBal.First().NewBalance;
                    }
                }
            }

            if (nextBalances != null && nextBalances.TurnoverDetails  != null && nextBalances.TurnoverDetails.Any())
            {
                foreach (var item in result.TurnoverDetails)
                {
                    var oldBal = nextBalances.TurnoverDetails.Where(x => x.ProductId == item.ProductId).ToList();
                    if (oldBal.Any())
                    {
                        item.NewBalance = oldBal.First().PreviousBalance;
                    }
                }
            }

            return result;
        }

        public static TurnoverDto GetDataForTurnoverFromDataAndShift(int backeryId, DateTime date, int shift)
        {
            var targetId = GetTurnoverIdFromDataAndShift(backeryId, date, shift);
            var result = new TurnoverDto
            {
                BackeryId = backeryId,
                Date = date,
                ShiftNo = shift,
                LastTurnoverId = targetId.Item1,
                TurnoverDetails = new List<TurnoverDetailDto>()
            };

            var oldBalances = GetBalancesForTurnoverId(backeryId, targetId.Item2);

            if (oldBalances.Any())
            {
                result.TurnoverDetails = oldBalances;
            }
            else
            {
                var dummyData = GetDataForNewTurnover(backeryId, date, shift);
                result.TurnoverDetails = dummyData.TurnoverDetails;
                result.LastTurnoverId = dummyData.Id;
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

        public static Tuple<int, int> GetTurnoverIdFromDataAndShift(int backeryId, DateTime date, int shift)
        {
            var id = 0;
            var lastId = 0;

            using (var db = new BackeryContext())
            {
                var t = db.Turnovers.Where(x => x.Date == date && x.BackeryId == backeryId && x.ShiftNo == shift);
                if (t.Any())
                {
                    var turnData = t.OrderByDescending(x => x.Id).FirstOrDefault();
                    id = turnData.Id;
                    lastId = turnData.LastTurnoverId;
                }
                return Tuple.Create(lastId, id);
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

        private static TurnoverDto GetPreviousTurnoverData(int backeryId, DateTime date, int shift)
        {
            var result = new TurnoverDto();
            var backery = GetBackery(backeryId);
            
            using (var db = new BackeryContext())
            {              
                result.BackeryId = backery.Id;
                result.ShiftNo = (shift == 1) ? backery.NumberOfShifts : shift - 1;
                result.Date = (shift == 1) ? date.AddDays(-1) : date;

                var t = db.Turnovers.Where(x => x.Date == result.Date && x.BackeryId == backeryId && x.ShiftNo == result.ShiftNo);
                if (t.Any())
                {
                    var turnData = t.OrderByDescending(x => x.Id).FirstOrDefault();
                    result.Id = turnData.Id;
                    result.TurnoverDetails = GetBalancesForTurnoverId(backeryId, turnData.Id);
                }
            }
            return result;
        }

        private static TurnoverDto GetNextTurnoverData(int backeryId, DateTime date, int shift)
        {
            var result = new TurnoverDto();
            var backery = GetBackery(backeryId);

            using (var db = new BackeryContext())
            {
                result.BackeryId = backery.Id;
                result.ShiftNo = (shift == backery.NumberOfShifts) ? 1 : shift + 1;
                result.Date = (shift == backery.NumberOfShifts) ? date.AddDays(1) : date;

                var t = db.Turnovers.Where(x => x.Date == result.Date && x.BackeryId == backeryId && x.ShiftNo == result.ShiftNo);
                if (t.Any())
                {
                    var turnData = t.OrderByDescending(x => x.Id).FirstOrDefault();
                    result.Id = turnData.Id;
                    result.TurnoverDetails = GetBalancesForTurnoverId(backeryId, turnData.Id);
                }
            }
            return result;
        }
    }
}