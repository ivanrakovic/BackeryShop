using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes
{
    public class TurnoverDetail
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id unosa")]
        public int TurnoverId { get; set; }

        [Required]
        [Display(Name = "Proizvod")]
        public int ProductId { get; set; }

        [Display(Name = "Naziv proizvoda")]
        public string ProductName { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Prethodno stanje")]
        public decimal PreviousBalance { get; set; }

        [Display(Name = "Došlo")]
        public decimal BakedNew { get; set; }

        [Display(Name = "Prodato")]
        public decimal Sold { get; set; }

        [Display(Name = "Škart")]
        public decimal Scrap { get; set; }

        [Display(Name = "Novo stanje")]
        public decimal NewBalance { get; set; }
        public Product Product { get; set; }


        public Turnover Turnover{ get; set; }
    }
}