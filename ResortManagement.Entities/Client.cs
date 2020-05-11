using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResortManagement.Entities
{
    public class Client
    {    [Key]
        public int ID { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
       
        public string Address { get; set; }
        public virtual List<Bookings> bookings { get; set; }

    }
}