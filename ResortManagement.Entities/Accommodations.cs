using System;
using System.Collections.Generic;
using System.Text;

namespace ResortManagement.Entities
{
     public class Accommodations
    {
        public int ID { get; set; }
        public int AccommodationGatgetID { get; set; }
        public AccommodationGatgets accommodationGatgets { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
