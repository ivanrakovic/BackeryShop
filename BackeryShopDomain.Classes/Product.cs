using System.Collections.Generic;

namespace BackeryShopDomain.Classes
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<PriceListDetail> PriceListDetail { get; set; }
	}
}