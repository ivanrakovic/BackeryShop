using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes
{
    public class TurnoverDetail
    {
        public int Id { get; set; }
        [Required]
        public int TurnoverId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal BakedNew { get; set; }
        public decimal Sold { get; set; }
        public decimal Scrap { get; set; }
        public decimal NewBalance { get; set; }
        public Product Product { get; set; }


        public Turnover Turnover{ get; set; }
    }
}