using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes
{
	public class PriceListDetail
	{
		public int Id { get; set; }

		[Range(0, 1000000)]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Display(Name = "Rb.")]
        public int OrderNo { get; set; }

		[Required]
        [Display(Name = "Cenovnik")]
        public int PriceListId { get; set; }
		public PriceList PriceList { get; set; }

		[Required]
        [Display(Name = "Proizvod")]
        public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}