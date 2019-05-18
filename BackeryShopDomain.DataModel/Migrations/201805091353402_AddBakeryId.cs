namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBakeryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turnovers", "BackeryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Turnovers", "BackeryId");
        }
    }
}
