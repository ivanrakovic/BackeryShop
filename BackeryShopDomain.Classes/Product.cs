using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackeryShopDomain.Classes
{
	public class Product
	{
		public int Id { get; set; }
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
		[Index("IX_ProductName", IsUnique = true)]
		public string Name { get; set; }

	    public bool Enabled { get; set; } = true;
        public ICollection<PriceListDetail> PriceListDetail { get; set; }
	    public ICollection<TurnoverDetail> TurnoverDetail { get; set; }
    }
}