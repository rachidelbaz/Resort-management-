namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCinBookinglistToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "clientId", "dbo.Clients");
            DropIndex("dbo.Bookings", new[] { "clientId" });
            AddColumn("dbo.Bookings", "RMUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "CIN", c => c.String(maxLength: 225));
            CreateIndex("dbo.Bookings", "RMUserId");
            CreateIndex("dbo.AspNetUsers", "CIN", unique: true);
            AddForeignKey("dbo.Bookings", "RMUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Bookings", "clientId");
            DropTable("dbo.Clients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        CIN = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.CIN);
            
            AddColumn("dbo.Bookings", "clientId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Bookings", "RMUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "CIN" });
            DropIndex("dbo.Bookings", new[] { "RMUserId" });
            DropColumn("dbo.AspNetUsers", "CIN");
            DropColumn("dbo.Bookings", "RMUserId");
            CreateIndex("dbo.Bookings", "clientId");
            AddForeignKey("dbo.Bookings", "clientId", "dbo.Clients", "CIN");
        }
    }
}
