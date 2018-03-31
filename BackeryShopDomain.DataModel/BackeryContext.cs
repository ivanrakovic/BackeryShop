using BackeryShopDomain.Classes;
using System.Data.Entity;

namespace BackeryShopDomain.DataModel
{
    public class BackeryContext: DbContext
    {
	    public DbSet<Backery> Backeries { get; set; }
	    public DbSet<PriceList> PriceLists { get; set; }
	    public DbSet<PriceListDetail> PriceListDetails { get; set; }
	    public DbSet<Product> Products { get; set; }
	}
}
