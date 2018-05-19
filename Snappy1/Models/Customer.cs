using System;
using System.Collections.Generic;

namespace Snappy1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Car = new HashSet<Car>();
            CarRental = new HashSet<CarRental>();
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Dob { get; set; }
        public string License { get; set; }

        public ICollection<Car> Car { get; set; }
        public ICollection<CarRental> CarRental { get; set; }
    }
}
