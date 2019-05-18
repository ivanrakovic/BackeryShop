namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTurnover : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TurnoverDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousBalance2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BanedNew = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Turnover_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Turnovers", t => t.Turnover_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Turnover_Id);
            
            CreateTable(
                "dbo.Turnovers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ShiftNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Backeries", "Turnover_Id", c => c.Int());
            CreateIndex("dbo.Backeries", "Turnover_Id");
            AddForeignKey("dbo.Backeries", "Turnover_Id", "dbo.Turnovers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TurnoverDetails", "Turnover_Id", "dbo.Turnovers");
            DropForeignKey("dbo.Backeries", "Turnover_Id", "dbo.Turnovers");
            DropForeignKey("dbo.TurnoverDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.TurnoverDetails", new[] { "Turnover_Id" });
            DropIndex("dbo.TurnoverDetails", new[] { "ProductId" });
            DropIndex("dbo.Backeries", new[] { "Turnover_Id" });
            DropColumn("dbo.Backeries", "Turnover_Id");
            DropTable("dbo.Turnovers");
            DropTable("dbo.TurnoverDetails");
        }
    }
}
