namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnabledProductProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Enabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Enabled");
        }
    }
}
