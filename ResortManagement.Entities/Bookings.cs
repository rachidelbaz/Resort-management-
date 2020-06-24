using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortManagement.Entities
{
     public class Bookings
    {
        [Key]
        public int ID { get; set; }
        public int AccommodationID { get; set; }
        [ForeignKey(name: "AccommodationID")]
        public Accommodations accommodation { get; set; }
        public DateTime AccommmodationDate { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = "Pending";
        public string RMUserId { get; set; }
        [ForeignKey(name: "RMUserId")]
        public RMUser RMUser { get; set; }
    }
}
