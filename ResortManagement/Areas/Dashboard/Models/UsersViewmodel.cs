using Microsoft.AspNet.Identity.EntityFramework;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class UsersViewmodel
    {
       public UserEditViewmodel userEditViewmodel =new UserEditViewmodel();
       public UsersListingViewmodel usersListingViewmodel = new UsersListingViewmodel();
       public IEnumerable<IdentityRole> Roles { get; set; }
    }
    public class UserEditViewmodel
    {
        public string UserID { get; set; }
        public RMUser RMUser { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        
    }
    public class UsersListingViewmodel
    {
        public Pager pager { get; set; }
        public IEnumerable<RMUser> RMUsers { get; set; }      
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get;  set; }
        public string RoleID { get; set; }
        public IEnumerable<IdentityRole> AllRoles { get; set; }
    }

}