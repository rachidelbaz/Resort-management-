using System;
using System.Collections.Generic;
using System.Text;

namespace ResortManagement.Entities
{
    public class AccommodationGatgets
    {
        public int ID { get; set; }
        public int AccommodationTypeID { get; set; }
        public AccommodationTypes accommodationType { get; set; }

        public string Name { get; set; }
        public int NOfRoom { get; set; }
        public decimal FeePerNight{ get; set; }

    }
}
