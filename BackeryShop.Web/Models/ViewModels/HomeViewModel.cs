using BackeryShopDomain.Classes.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BackeryShop.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BackeryDto> BackeriesList { get; set; }
        public bool ShowTurnoverInputs => BackeriesList.Any();
    }
}