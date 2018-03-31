using System.Collections.Generic;

namespace BackeryShopDomain.Classes
{
	public class PriceList
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<PriceListDetail> PriceListDetail { get; set; }
		public ICollection<Backery> Backery { get; set; }
	}
}