using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackeryShopDomain.Classes;

namespace BackeryShop.Web.Models.ViewModels
{
    public class TurnoverViewModel
    {
        public TurnoverViewModel()
        {
            TurnoverProductsViewModels = new List<TurnoverProductViewModel>();
        }

        public Backery Backery { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public List<TurnoverProductViewModel> TurnoverProductsViewModels { get; set; }

    }
}