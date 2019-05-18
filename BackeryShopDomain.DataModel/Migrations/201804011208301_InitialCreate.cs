namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Backeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        NumberOfShifts = c.Int(nullable: false),
                        PriceListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceLists", t => t.PriceListId)
                .Index(t => t.Name, unique: true, name: "BakeryNameIndex")
                .Index(t => t.PriceListId);
            
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceListDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderNo = c.Int(nullable: false),
                        PriceListId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceLists", t => t.PriceListId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.PriceListId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Backeries", "PriceListId", "dbo.PriceLists");
            DropForeignKey("dbo.PriceListDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PriceListDetails", "PriceListId", "dbo.PriceLists");
            DropIndex("dbo.PriceListDetails", new[] { "ProductId" });
            DropIndex("dbo.PriceListDetails", new[] { "PriceListId" });
            DropIndex("dbo.Backeries", new[] { "PriceListId" });
            DropIndex("dbo.Backeries", "BakeryNameIndex");
            DropTable("dbo.Products");
            DropTable("dbo.PriceListDetails");
            DropTable("dbo.PriceLists");
            DropTable("dbo.Backeries");
        }
    }
}
