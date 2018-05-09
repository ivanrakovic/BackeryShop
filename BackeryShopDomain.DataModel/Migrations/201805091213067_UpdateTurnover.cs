namespace BackeryShopDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTurnover : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TurnoverDetails", "NewBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TurnoverDetails", "BakedNew", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TurnoverDetails", "Scrap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TurnoverDetails", "PreviousBalance2");
            DropColumn("dbo.TurnoverDetails", "BanedNew");
            DropColumn("dbo.TurnoverDetails", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TurnoverDetails", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TurnoverDetails", "BanedNew", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TurnoverDetails", "PreviousBalance2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TurnoverDetails", "Scrap");
            DropColumn("dbo.TurnoverDetails", "BakedNew");
            DropColumn("dbo.TurnoverDetails", "NewBalance");
        }
    }
}
