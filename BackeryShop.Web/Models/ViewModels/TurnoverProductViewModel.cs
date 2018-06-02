using System.Collections.Generic;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShop.Web.Models.ViewModels
{
    public class TurnoverProductViewModel
    {
        public TurnoverProductViewModel()
        {
            ProductsData = new List<TurnoverDetailDto>();
        }
        public int TurnoverId { get; set; }
        public int LastTurnoverId { get; set; }
        public IEnumerable<TurnoverDetailDto> ProductsData { get; set; }
        public bool IsEditMode { get; set; }
    }
}