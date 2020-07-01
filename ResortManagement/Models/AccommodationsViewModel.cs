using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Models
{
    public class AccommodationsViewModel
    {
        public DateTime CheckOut;
        public DateTime CheckIn;
       
        public int Adults;
        public int Children;

        public IEnumerable<Accommodations> accommodations { get; set; }
        public IEnumerable<AccommodationGatgets> accommodationGatgets { get; set; }
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set; }
        public List<Picture> Accommopictures { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public Pager pager { get; set; }
        public int Duration { get;set; }
        public int? NoOfBeds { get;set; }
    }
    public class AccommodationsGasgetsViewModel {
    
    public AccommodationGatgets accommodationGatgets { get; set; }
   
    }
    public class AccommodationTypesViewModel {
        public AccommodationTypes accommodationTypes { get; set; }
    }


}