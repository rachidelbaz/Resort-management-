namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCinToClientTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "clientId", "dbo.Clients");
            DropIndex("dbo.Bookings", new[] { "clientId" });
            DropPrimaryKey("dbo.Clients");
            AddColumn("dbo.Clients", "CIN", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bookings", "clientId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Clients", "CIN");
            CreateIndex("dbo.Bookings", "clientId");
            AddForeignKey("dbo.Bookings", "clientId", "dbo.Clients", "CIN");
            DropColumn("dbo.Clients", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Bookings", "clientId", "dbo.Clients");
            DropIndex("dbo.Bookings", new[] { "clientId" });
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Bookings", "clientId", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "CIN");
            AddPrimaryKey("dbo.Clients", "ID");
            CreateIndex("dbo.Bookings", "clientId");
            AddForeignKey("dbo.Bookings", "clientId", "dbo.Clients", "ID", cascadeDelete: true);
        }
    }
}
