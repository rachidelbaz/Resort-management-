using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommondationGadgetsViewModel
    {
        public AccommodationGadgetsEditViewModel accommodationGadgetsEditViewModel = new AccommodationGadgetsEditViewModel();
        public AccommodationGadgetsListingViewModel accommodationGadgetsListingViewModel = new AccommodationGadgetsListingViewModel();
    }

    public class AccommodationGadgetsEditViewModel
    {
        public AccommodationGatgets accommodationGadgets { get; set; }
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set; }

    }

    public class AccommodationGadgetsListingViewModel
    {
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set; }
        public IEnumerable<AccommodationGatgets> accommodationGadgets { get; set; }
        public Pager pager { get; set; }
        public int PageNo { get; set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
    }

}