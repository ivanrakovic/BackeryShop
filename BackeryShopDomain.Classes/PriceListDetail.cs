using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes
{
	public class PriceListDetail
	{
		public int Id { get; set; }

		[Range(0, 1000000)]
		public decimal Price { get; set; }
		public int OrderNo { get; set; }

		[Required]
		public int PriceListId { get; set; }
		public PriceList PriceList { get; set; }

		[Required]
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}