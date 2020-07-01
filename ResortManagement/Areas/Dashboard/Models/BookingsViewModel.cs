using ResortManagement.Areas.Code;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class BookingsViewModel
    {
        public BookingsEditViewModel bookingsEditViewModel = new BookingsEditViewModel();
        public BookingsListingViewModel  bookingsListingViewModel = new BookingsListingViewModel(); 
        public Status status;
    }
    public class BookingsEditViewModel
    {
        public Bookings booking { get; set; }
        public string Accommodation { get; set; }
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set;}
    }
    public class BookingsListingViewModel
    {
        public Pager pager;
        public int PageSize { get;  set; }
        public string SearchTerm { get;  set; }
        public int PageNo { get;  set; }
        public IEnumerable<Bookings> Bookings { get;  set; }
        public string Status { get; set; }
    }
}