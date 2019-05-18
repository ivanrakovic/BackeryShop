namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBakeryId1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Backeries", "Turnover_Id", "dbo.Turnovers");
            DropIndex("dbo.Backeries", new[] { "Turnover_Id" });
            CreateTable(
                "dbo.TurnoverBackeries",
                c => new
                    {
                        Turnover_Id = c.Int(nullable: false),
                        Backery_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Turnover_Id, t.Backery_Id })
                .ForeignKey("dbo.Turnovers", t => t.Turnover_Id, cascadeDelete: true)
                .ForeignKey("dbo.Backeries", t => t.Backery_Id, cascadeDelete: true)
                .Index(t => t.Turnover_Id)
                .Index(t => t.Backery_Id);
            
            DropColumn("dbo.Backeries", "Turnover_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Backeries", "Turnover_Id", c => c.Int());
            DropForeignKey("dbo.TurnoverBackeries", "Backery_Id", "dbo.Backeries");
            DropForeignKey("dbo.TurnoverBackeries", "Turnover_Id", "dbo.Turnovers");
            DropIndex("dbo.TurnoverBackeries", new[] { "Backery_Id" });
            DropIndex("dbo.TurnoverBackeries", new[] { "Turnover_Id" });
            DropTable("dbo.TurnoverBackeries");
            CreateIndex("dbo.Backeries", "Turnover_Id");
            AddForeignKey("dbo.Backeries", "Turnover_Id", "dbo.Turnovers", "Id");
        }
    }
}
