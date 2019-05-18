namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTurnover1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TurnoverBackeries", "Turnover_Id", "dbo.Turnovers");
            DropForeignKey("dbo.TurnoverBackeries", "Backery_Id", "dbo.Backeries");
            DropIndex("dbo.TurnoverBackeries", new[] { "Turnover_Id" });
            DropIndex("dbo.TurnoverBackeries", new[] { "Backery_Id" });
            CreateIndex("dbo.Turnovers", "BackeryId");
            AddForeignKey("dbo.Turnovers", "BackeryId", "dbo.Backeries", "Id");
            DropTable("dbo.TurnoverBackeries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TurnoverBackeries",
                c => new
                    {
                        Turnover_Id = c.Int(nullable: false),
                        Backery_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Turnover_Id, t.Backery_Id });
            
            DropForeignKey("dbo.Turnovers", "BackeryId", "dbo.Backeries");
            DropIndex("dbo.Turnovers", new[] { "BackeryId" });
            CreateIndex("dbo.TurnoverBackeries", "Backery_Id");
            CreateIndex("dbo.TurnoverBackeries", "Turnover_Id");
            AddForeignKey("dbo.TurnoverBackeries", "Backery_Id", "dbo.Backeries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TurnoverBackeries", "Turnover_Id", "dbo.Turnovers", "Id", cascadeDelete: true);
        }
    }
}
