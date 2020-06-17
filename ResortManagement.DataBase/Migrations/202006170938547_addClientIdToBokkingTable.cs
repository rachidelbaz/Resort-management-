namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addClientIdToBokkingTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Client_ID", "dbo.Clients");
            DropIndex("dbo.Bookings", new[] { "Client_ID" });
            RenameColumn(table: "dbo.Bookings", name: "Client_ID", newName: "clientId");
            AlterColumn("dbo.Bookings", "clientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "clientId");
            AddForeignKey("dbo.Bookings", "clientId", "dbo.Clients", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "clientId", "dbo.Clients");
            DropIndex("dbo.Bookings", new[] { "clientId" });
            AlterColumn("dbo.Bookings", "clientId", c => c.Int());
            RenameColumn(table: "dbo.Bookings", name: "clientId", newName: "Client_ID");
            CreateIndex("dbo.Bookings", "Client_ID");
            AddForeignKey("dbo.Bookings", "Client_ID", "dbo.Clients", "ID");
        }
    }
}
