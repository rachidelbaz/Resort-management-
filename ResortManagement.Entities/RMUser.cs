using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ResortManagement.Entities
{
    public class RMUser: IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<RMUser> manager)
      {
          // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
          var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
          // Add custom user claims here
           return userIdentity;
      }
       
    }
    
}
