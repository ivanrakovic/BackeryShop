using System;
using System.Collections.Generic;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShop.Web.Models.ViewModels
{
    public class TurnoverViewModel
    {
        public TurnoverViewModel()
        {
            TurnoverProductsViewModels = new List<TurnoverDetailDto>();
        }

        public BackeryDto Backery { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public int LastTurnoverId { get; set; }
        public List<TurnoverDetailDto> TurnoverProductsViewModels { get; set; }
        public bool IsEditMode { get; set; }

    }
}