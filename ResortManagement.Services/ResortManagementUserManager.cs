using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ResortManagement.DataBase;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Services
{
    public class ResortManagementUserManager : UserManager<RMUser>
    {
        public ResortManagementUserManager(IUserStore<RMUser> store)
           : base(store)
        {
        }
        public static ResortManagementUserManager Create(IdentityFactoryOptions<ResortManagementUserManager> options, IOwinContext context)
        {
            var manager = new ResortManagementUserManager(new UserStore<RMUser>(context.Get<ResortManagementDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<RMUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<RMUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<RMUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            // manager.EmailService = new EmailService();
           // manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<RMUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            using (var context=new ResortManagementDbContext())
            {
                var rolesStore = new RoleStore<IdentityRole>(context);
                var rolesMgr = new RoleManager<IdentityRole>(rolesStore);
                return rolesMgr.Roles.ToList();
            }
        }

        public RMUser GetUserByID(string value)
        {
            using (var context=new ResortManagementDbContext())
            {
                return context.Users.Find(value);
            }
        }

        public int GetUsersCount(string searchTerm, string roleID)
        {
            using (var context = new ResortManagementDbContext())
            {
                var users = context.Users.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = users.Where(u => u.UserName.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if (!string.IsNullOrEmpty(roleID))
                {
                    //users = users.Where(u =>u..ToLower().Contains(SearchTerm.Trim().ToLower()));
                }
                return users.Count();
            }
        }

        public async Task<bool> DeleteUserByID(string value)
        {
            using (var context=new ResortManagementDbContext())
            {
                var model = GetUserByID(value);
                context.Users.Attach(model);
                context.Users.Remove(model);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public IEnumerable<RMUser> GetUsers(string SearchTerm, string RoleID, int pageSize, int pageNo)
        {
            using (var context = new ResortManagementDbContext())
            {
                var users = context.Users.AsQueryable();
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    users = users.Where(u => u.UserName.ToLower().Contains(SearchTerm.Trim().ToLower()));
                }
                if (!string.IsNullOrEmpty(RoleID))
                {
                    //users = users.Where(u=>u.);
                }
                return users.OrderByDescending(u=>u.Id).Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            }

        }
    }
}
