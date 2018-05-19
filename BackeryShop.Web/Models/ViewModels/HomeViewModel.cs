using BackeryShopDomain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackeryShop.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Backery> BackeriesList { get; set; }
    }
}