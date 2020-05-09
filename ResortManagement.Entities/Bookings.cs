using System;
using System.Collections.Generic;
using System.Text;

namespace ResortManagement.Entities
{
    public class Bookings
    {
        public int ID { get; set; }
        public int AccommodationID { get; set; }
        public Accommodations accommodations { get; set; }
        public DateTime AccommmodationDate { get; set; }
        public int Duration { get; set; }
        public int ClientID { get; set; }
        public Client client { get; set; }

    }
}
