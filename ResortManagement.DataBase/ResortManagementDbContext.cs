using Microsoft.AspNet.Identity.EntityFramework;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.DataBase
{
    public class ResortManagementDbContext : IdentityDbContext<RMUser>
    {
        public ResortManagementDbContext() : base("RMDb")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ResortManagementDbContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ResortManagementDbContext>());
           Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ResortManagementDbContext>());
            this.Configuration.LazyLoadingEnabled = false;
           
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
        //    modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
        //    modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        //    modelBuilder.Entity<AccommodationPicture>().HasKey(ap => new { ap.AccommodationID, ap.pictureID });
        //}
        public static ResortManagementDbContext Create()
        {
            return new ResortManagementDbContext();
        }
        public DbSet<AccommodationTypes> accommodationType { get; set; }
        public DbSet<AccommodationGatgets> accommodationGatget { get; set; }
        public DbSet<Accommodations> accommodation { get; set; }
        public DbSet<Bookings> booking { get; set; }
        //should not create user cuase it aready created from IdentityDbContext<user> 
        //public DbSet<RMUser> users { get; set; }
        public DbSet<Picture>  picture { get; set; }
    }
}
