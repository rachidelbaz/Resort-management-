namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPictureIdColumnToAccoType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccommodationTypePictures", "picture_ID", "dbo.Pictures");
            DropIndex("dbo.AccommodationTypePictures", new[] { "picture_ID" });
            RenameColumn(table: "dbo.AccommodationTypePictures", name: "picture_ID", newName: "pictureID");
            AlterColumn("dbo.AccommodationTypePictures", "pictureID", c => c.Int(nullable: false));
            CreateIndex("dbo.AccommodationTypePictures", "pictureID");
            AddForeignKey("dbo.AccommodationTypePictures", "pictureID", "dbo.Pictures", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccommodationTypePictures", "pictureID", "dbo.Pictures");
            DropIndex("dbo.AccommodationTypePictures", new[] { "pictureID" });
            AlterColumn("dbo.AccommodationTypePictures", "pictureID", c => c.Int());
            RenameColumn(table: "dbo.AccommodationTypePictures", name: "pictureID", newName: "picture_ID");
            CreateIndex("dbo.AccommodationTypePictures", "picture_ID");
            AddForeignKey("dbo.AccommodationTypePictures", "picture_ID", "dbo.Pictures", "ID");
        }
    }
}
