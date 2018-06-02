using System;
using System.Collections.Generic;
using BackeryShopDomain.Classes.Entities;

namespace BackeryShop.Web.Models.ViewModels
{
    public class TurnoverViewModel
    {
        public BackeryDto Backery { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public TurnoverProductViewModel ProductList { get; set; }
    }
}