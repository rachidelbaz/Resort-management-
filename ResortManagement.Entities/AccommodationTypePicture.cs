using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Entities
{
     public class AccommodationTypePicture
    {
        [Key]
        public int ID { get; set; }
        public int AccommodationTypeId { get; set; }
        [ForeignKey("AccommodationTypeId")]
        public AccommodationTypes  accommodationTypes { get; set; }
        public virtual Picture picture { get; set; }

    }
}
