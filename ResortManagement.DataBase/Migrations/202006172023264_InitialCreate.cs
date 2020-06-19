namespace ResortManagement.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250, unicode: false),
                        AccommodationGatgetID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationGatgets", t => t.AccommodationGatgetID, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.AccommodationGatgetID);
            
            CreateTable(
                "dbo.AccommodationGatgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        NOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccommodationTypeID, cascadeDelete: true)
                .Index(t => t.AccommodationTypeID);
            
            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccommodationTypePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationTypeId = c.Int(nullable: false),
                        pictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccommodationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.pictureID, cascadeDelete: true)
                .Index(t => t.AccommodationTypeId)
                .Index(t => t.pictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccommodationGadgetPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationGadgetId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationGatgets", t => t.AccommodationGadgetId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.AccommodationGadgetId)
                .Index(t => t.PictureId);
            
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
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationID = c.Int(nullable: false),
                        AccommmodationDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Status = c.String(),
                        clientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.clientId, cascadeDelete: true)
                .Index(t => t.AccommodationID)
                .Index(t => t.clientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookings", "clientId", "dbo.Clients");
            DropForeignKey("dbo.Bookings", "AccommodationID", "dbo.Accommodations");
            DropForeignKey("dbo.AccommodationPictures", "pictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccommodationPictures", "AccommodationID", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AccommodationGatgetID", "dbo.AccommodationGatgets");
            DropForeignKey("dbo.AccommodationGadgetPictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.AccommodationGadgetPictures", "AccommodationGadgetId", "dbo.AccommodationGatgets");
            DropForeignKey("dbo.AccommodationGatgets", "AccommodationTypeID", "dbo.AccommodationTypes");
            DropForeignKey("dbo.AccommodationTypePictures", "pictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccommodationTypePictures", "AccommodationTypeId", "dbo.AccommodationTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Bookings", new[] { "clientId" });
            DropIndex("dbo.Bookings", new[] { "AccommodationID" });
            DropIndex("dbo.AccommodationPictures", new[] { "pictureID" });
            DropIndex("dbo.AccommodationPictures", new[] { "AccommodationID" });
            DropIndex("dbo.AccommodationGadgetPictures", new[] { "PictureId" });
            DropIndex("dbo.AccommodationGadgetPictures", new[] { "AccommodationGadgetId" });
            DropIndex("dbo.AccommodationTypePictures", new[] { "pictureID" });
            DropIndex("dbo.AccommodationTypePictures", new[] { "AccommodationTypeId" });
            DropIndex("dbo.AccommodationGatgets", new[] { "AccommodationTypeID" });
            DropIndex("dbo.Accommodations", new[] { "AccommodationGatgetID" });
            DropIndex("dbo.Accommodations", new[] { "Name" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Clients");
            DropTable("dbo.Bookings");
            DropTable("dbo.AccommodationPictures");
            DropTable("dbo.AccommodationGadgetPictures");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccommodationTypePictures");
            DropTable("dbo.AccommodationTypes");
            DropTable("dbo.AccommodationGatgets");
            DropTable("dbo.Accommodations");
        }
    }
}
