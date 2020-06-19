using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Entities
{
    public class Accommodations
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName="VARCHAR")]
        [StringLength(250)]
        [Index(IsUnique =true)]
        public string Name { get; set; }
        public int AccommodationGatgetID { get; set; }
        [ForeignKey(name: "AccommodationGatgetID")]
        public AccommodationGatgets accommodationGatgets { get; set; }
        public string Description { get; set; }
        public virtual List<AccommodationPicture>  accommodationPictures { get; set; }
        
    }
}
