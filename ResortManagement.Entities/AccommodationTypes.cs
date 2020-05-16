using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortManagement.Entities
{
     public class AccommodationTypes
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
