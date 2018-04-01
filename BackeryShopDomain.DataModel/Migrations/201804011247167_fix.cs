namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Backeries", name: "BakeryNameIndex", newName: "IX_BakeryName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Backeries", name: "IX_BakeryName", newName: "BakeryNameIndex");
        }
    }
}
