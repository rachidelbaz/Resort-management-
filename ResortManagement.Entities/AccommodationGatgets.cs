using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Entities
{
    public class AccommodationGatgets
    {
        [Key]
        public int ID { get; set; }
        public int AccommodationTypeID { get; set; }
        [ForeignKey(name: "AccommodationTypeID")]
        public virtual AccommodationTypes accommodationType { get; set; }

        public string Name { get; set; }
        public int NOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public virtual List<AccommodationGadgetPicture> GadgetPictures { get; set; }
    }
}
