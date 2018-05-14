using BackeryShopDomain.Classes;

namespace BackeryShop.Web.Models.ViewModels
{
    public class TurnoverProductViewModel
    {
        public int ProdtId { get; set;}
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousBalance { get; set; }
    }
}