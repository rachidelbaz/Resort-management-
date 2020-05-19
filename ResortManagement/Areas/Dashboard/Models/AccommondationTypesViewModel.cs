using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
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
    }
   
}