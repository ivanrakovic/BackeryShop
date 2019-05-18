namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationsAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceLists", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 100));
            CreateIndex("dbo.PriceLists", "Name", unique: true, name: "IX_PriceListName");
            CreateIndex("dbo.Products", "Name", unique: true, name: "IX_ProductName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", "IX_ProductName");
            DropIndex("dbo.PriceLists", "IX_PriceListName");
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.PriceLists", "Name", c => c.String());
        }
    }
}
