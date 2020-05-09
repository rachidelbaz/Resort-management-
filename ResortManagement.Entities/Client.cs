using System;
using System.Collections.Generic;
using System.Text;

namespace ResortManagement.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

    }
}