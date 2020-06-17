using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortManagement.Entities
{
    public class AccommodationPicture
    {
        [Key]
        public int ID { get; set; }
        public int AccommodationID { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Accommodations accommodations { get; set; }
        public int pictureID { get; set; }
        public virtual Picture picture { get; set; }
    }
}