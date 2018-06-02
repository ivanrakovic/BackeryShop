using System;
using System.Collections.Generic;
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

        public static TurnoverDto GetNextTurnoverDataForBakery(int backeryId)
        {
            var lastId = TurnoverRepository.GetLastTurnoverId(backeryId);
            return TurnoverRepository.GetNextTurnoverDataFromTurnoverId(backeryId, lastId);
        }
    }
}