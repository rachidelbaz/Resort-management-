using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ResortManagement.Entities;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class RolesViewModel
    {
        public EditRolesViewModel editRolesViewModel = new EditRolesViewModel();
        public ListingRolesViewModel listingRolesViewModel = new ListingRolesViewModel();
    }
    public class EditRolesViewModel
    {
        public IdentityRole role { get; set; }
    }
    public class ListingRolesViewModel
    {
        public Pager pager { get; set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public IEnumerable<IdentityRole> Roles { get;set; }
    }
}