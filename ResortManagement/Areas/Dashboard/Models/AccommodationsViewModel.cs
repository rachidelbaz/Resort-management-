using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommodationsViewModel
    {
        public AccommodationsListingViewModel accommodationsListing = new AccommodationsListingViewModel();
        public AccommodationsEditViewModel accommodationsEdit = new AccommodationsEditViewModel();
    }
    public class AccommodationsEditViewModel
    {
        public Accommodations accommodations = new Accommodations();
        public IEnumerable<Accommodations> accommodationsList { get; set; }
    }
    public class AccommodationsListingViewModel
    {
        public AccommodationGatgets accommodationGatgets { get; set; }
        public string AccommodationGatgets { get;  set; }
        public IEnumerable<Accommodations> accommodations { get; set; }
        public Pager pager { get; set; }
        public string SearchTerm { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

    }
}