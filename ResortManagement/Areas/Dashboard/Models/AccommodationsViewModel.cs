using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommodationActionModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccommodationGatgetID { get; set; }
        public string imgUrls { get; set; }
    }
    public class AccommodationsViewModel
    {
        public AccommodationsListingViewModel accommodationsListing = new AccommodationsListingViewModel();
        public AccommodationsEditViewModel accommodationEdit = new AccommodationsEditViewModel();
        public IEnumerable< AccommodationGatgets> accommodationGatgets { get; set; }
    }
    public class AccommodationsEditViewModel
    {
        public List<Picture> Pictures { get; set;}
        public Accommodations accommodation {get ; set;}
        public IEnumerable<Accommodations> accommodationsList { get; set;}
        public IEnumerable<AccommodationGatgets> accommodationGatgets { get; set;}
    }
    public class AccommodationsListingViewModel
    {
        public int AccommodationGatgetID { get;  set; }
        public IEnumerable<Accommodations> accommodations { get; set; }
        public Pager pager { get; set; }
        public string SearchTerm { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

    }
}