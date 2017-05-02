namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "city", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "city");
        }
    }
}
