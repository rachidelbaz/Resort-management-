namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationID = c.Int(nullable: false),
                        pictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationID, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.pictureID, cascadeDelete: true)
                .Index(t => t.AccommodationID)
                .Index(t => t.pictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccommodationPictures", "pictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccommodationPictures", "AccommodationID", "dbo.Accommodations");
            DropIndex("dbo.AccommodationPictures", new[] { "pictureID" });
            DropIndex("dbo.AccommodationPictures", new[] { "AccommodationID" });
            DropTable("dbo.Pictures");
            DropTable("dbo.AccommodationPictures");
        }
    }
}
