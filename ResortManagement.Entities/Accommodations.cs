using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ResortManagement.Entities
{
     public class Accommodations
    {   [Key]
        public int ID { get; set; }
        public int AccommodationGatgetID { get; set; }
        [ForeignKey(name: "AccommodationGatgetID")]
        public virtual AccommodationGatgets accommodationGatgets { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
