using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ResortManagement.DataBase;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Services
{
     public class ResortManagementRoleManager:RoleManager<IdentityRole>
    {
        public ResortManagementRoleManager(IRoleStore<IdentityRole,string> roleStore):base(roleStore)
        {

        }

        public static ResortManagementRoleManager Create(IdentityFactoryOptions<ResortManagementRoleManager> options,IOwinContext context)
        {
            return new ResortManagementRoleManager(new RoleStore<IdentityRole>(context.Get<ResortManagementDbContext>()));
        }

        public IEnumerable<IdentityRole> SearchRoles(string searchTerm,int pageSize, int pageNo)
        {
            using (var context=new ResortManagementDbContext())
            {
                var roles = context.Roles.Include(r=>r.Users).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                   roles=roles.Include(r=>r.Users).Where(r=>r.Name.ToLower().Contains(searchTerm.ToLower()));
                }

               return roles.OrderByDescending(r => r.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int RolesCount(string searchTerm)
        {
            using (var context = new ResortManagementDbContext())
            {
                var roles = context.Roles.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                   roles=roles.Where(r => r.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
                }

                return roles.Count();
            }
        }
    }
}
