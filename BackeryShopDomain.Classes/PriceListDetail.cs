namespace BackeryShopDomain.Classes
{
	public class PriceListDetail
	{
		public int Id { get; set; }

		public decimal Price { get; set; }
		public int OrderNo { get; set; }

		public int PriceListId { get; set; }
		public PriceList PriceList { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}