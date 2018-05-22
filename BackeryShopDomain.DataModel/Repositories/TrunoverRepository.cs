using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BackeryShopDomain.Classes;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShopDomain.DataModel.Repositories
{
    public static class TrunoverRepository
    {
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
    }
}