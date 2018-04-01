using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackeryShopDomain.Classes
{
	public class PriceList
	{
		public int Id { get; set; }

		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
		[Index("IX_PriceListName", IsUnique = true)]
		public string Name { get; set; }

		public ICollection<PriceListDetail> PriceListDetail { get; set; }
		public ICollection<Backery> Backery { get; set; }
	}
}