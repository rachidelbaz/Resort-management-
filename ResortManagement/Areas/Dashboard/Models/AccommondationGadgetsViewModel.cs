using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommodationGatgetsActionModel
    {
        public int ID { get; set; }
        public int AccommodationTypeID { get; set; }
        public string Name { get; set; }
        public int NOfRoom { get; set; }
        public int NofBeds { get; set; }
        public decimal FeePerNight { get; set; }
        public string imgUrls { get; set; }

    }
    public class AccommondationGadgetsViewModel
    {

        public virtual List<AccommodationGadgetPicture> GadgetPictures { get; set; }
        public AccommodationGadgetsEditViewModel accommodationGadgetsEditViewModel = new AccommodationGadgetsEditViewModel();
        public AccommodationGadgetsListingViewModel accommodationGadgetsListingViewModel = new AccommodationGadgetsListingViewModel();
    }

    public class AccommodationGadgetsEditViewModel
    {
        public List<Picture> pictures { get; set; }
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
        public int AccomoodationType { get; set; }
    }

}