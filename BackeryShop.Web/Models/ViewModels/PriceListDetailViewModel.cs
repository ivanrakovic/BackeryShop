using BackeryShopDomain.Classes;
using System.Collections.Generic;

namespace BackeryShop.Web.Models.ViewModels
{
    public class PriceListDetailViewModel
    {
        public int MasterId { get; set; }
        public IEnumerable<PriceListDetail> PriceListDetailItems { get; set; }
    }
}