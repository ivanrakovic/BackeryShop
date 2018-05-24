namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLastTurnoverId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turnovers", "LastTurnoverId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Turnovers", "LastTurnoverId");
        }
    }
}
