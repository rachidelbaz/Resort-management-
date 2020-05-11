using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ResortManagement.Entities
{
    public class Bookings
    {   [Key]
        public int ID { get; set; }
        public int AccommodationID { get; set; }
        [ForeignKey(name: "AccommodationID")]
        public virtual Accommodations accommodation { get; set; }
        public DateTime AccommmodationDate { get; set; }
        public int Duration { get; set; }
      
    }
}
