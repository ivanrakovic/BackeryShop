namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTurnover2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TurnoverDetails", new[] { "Turnover_Id" });
            RenameColumn(table: "dbo.TurnoverDetails", name: "Turnover_Id", newName: "TurnoverId");
            AlterColumn("dbo.TurnoverDetails", "TurnoverId", c => c.Int(nullable: false));
            CreateIndex("dbo.TurnoverDetails", "TurnoverId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TurnoverDetails", new[] { "TurnoverId" });
            AlterColumn("dbo.TurnoverDetails", "TurnoverId", c => c.Int());
            RenameColumn(table: "dbo.TurnoverDetails", name: "TurnoverId", newName: "Turnover_Id");
            CreateIndex("dbo.TurnoverDetails", "Turnover_Id");
        }
    }
}
