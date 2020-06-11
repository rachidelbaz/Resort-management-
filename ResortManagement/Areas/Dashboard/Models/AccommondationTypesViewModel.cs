using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommondationTypesActionViewModel
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string imgUrls { get; set; }

    }
    public class AccommondationTypesViewModel
    {
        public AccommondationListingTypesViewModel accommondationListingTypesViewModel = new AccommondationListingTypesViewModel();
        public AccommondationTypeEditViewModel accommondationTypeEditViewModel = new AccommondationTypeEditViewModel();
    }
    public class AccommondationListingTypesViewModel
    {
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set; }
        public string SearchTerm { get; set; }
        public int PageNo { get; set; }
        public Pager pager { get; set; }
        public int PageSize { get; set; }
    }
    public class AccommondationTypeEditViewModel
    {
        public AccommodationTypes accommodationType { get; set; }
        public List<Picture> pictures { get; set; }
    }
   
}