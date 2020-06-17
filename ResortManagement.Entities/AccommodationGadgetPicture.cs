using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Entities
{
    public class AccommodationGadgetPicture
    {
        [Key]
        public int ID { get; set; }
        public int AccommodationGadgetId { get; set; }
        [ForeignKey("AccommodationGadgetId")]
        public virtual AccommodationGatgets  accommodationGatgets { get; set; }

        public int PictureId { get; set; }
        public virtual Picture picture { get; set; }
    }
}
