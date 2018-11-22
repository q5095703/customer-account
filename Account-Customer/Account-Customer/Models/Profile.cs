using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCustomer.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool isDeleted { get; set; }
    }
}
