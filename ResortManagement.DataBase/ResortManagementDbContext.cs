using Microsoft.AspNet.Identity.EntityFramework;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ResortManagement.DataBase
{
    public class ResortManagementDbContext : IdentityDbContext<RMUser>
    {
        public ResortManagementDbContext() : base("ResortManagementDb")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ResortManagementDbContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ResortManagementDbContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ResortManagementDbContext>());
            this.Configuration.LazyLoadingEnabled = false;

        }
        public static ResortManagementDbContext Create()
        {
            return new ResortManagementDbContext();
        }
        public DbSet<Accommodations> accommodation { get; set; }
        public DbSet<AccommodationGatgets> accommodationGatget { get; set; }

        public DbSet<AccommodationTypes> accommodationType { get; set; }
        public DbSet<Bookings> booking { get; set; }
        public DbSet<Client> client { get; set; }
        //should not create user cuase it aready created from IdentityDbContext<user> 
        //public DbSet<RMUser> users { get; set; }


    }
}
