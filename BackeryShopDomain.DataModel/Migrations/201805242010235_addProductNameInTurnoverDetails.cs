namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductNameInTurnoverDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TurnoverDetails", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TurnoverDetails", "ProductName");
        }
    }
}
