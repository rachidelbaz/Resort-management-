using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Services
{
    public class ResortManagemenetSignInManager:SignInManager<RMUser, string>
    {
        public ResortManagemenetSignInManager(ResortManagementUserManager userManager, IAuthenticationManager authenticationManager)
           : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(RMUser user)
        {
            return user.GenerateUserIdentityAsync((ResortManagementUserManager)UserManager);
        }

        public static ResortManagemenetSignInManager Create(IdentityFactoryOptions<ResortManagemenetSignInManager> options, IOwinContext context)
        {
            return new ResortManagemenetSignInManager(context.GetUserManager<ResortManagementUserManager>(), context.Authentication);
        }
    }
}
