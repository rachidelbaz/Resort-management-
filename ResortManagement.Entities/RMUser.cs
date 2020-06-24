using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Entities
{
    public class RMUser : IdentityUser
    {
        [StringLength(225)]
        [Index(IsUnique =true)]
        public string CIN { get; set; }
        public IList<string> UserRoles { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string  City { get; set; }
        public virtual List<Bookings> bookings { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<RMUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
