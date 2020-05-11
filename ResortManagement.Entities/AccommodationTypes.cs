using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResortManagement.Entities
{
    public class AccommodationTypes
    {   [Key]
        public int ID { get; set; }
        public string Type { get; set; }
    }
}
