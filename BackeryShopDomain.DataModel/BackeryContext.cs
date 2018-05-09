using BackeryShopDomain.Classes;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BackeryShopDomain.DataModel
{
    public class BackeryContext: DbContext
    {
	    public DbSet<Backery> Backeries { get; set; }
	    public DbSet<PriceList> PriceLists { get; set; }
	    public DbSet<PriceListDetail> PriceListDetails { get; set; }
	    public DbSet<Product> Products { get; set; }
        public DbSet<Turnover> Turnovers { get; set; }
        public DbSet<TurnoverDetail> TurnoverDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
		    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}
	}
}
