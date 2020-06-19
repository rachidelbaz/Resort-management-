namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNOFBedsTotableAccommodation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccommodationGatgets", "NOFBeds", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccommodationGatgets", "NOFBeds");
        }
    }
}
