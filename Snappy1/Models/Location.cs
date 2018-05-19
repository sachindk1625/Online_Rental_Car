using System;
using System.Collections.Generic;

namespace Snappy1.Models
{
    public partial class Location
    {
        public Location()
        {
            Car = new HashSet<Car>();
            CarRental = new HashSet<CarRental>();
        }

        public string LocationName { get; set; }
        public string ManagerUsername { get; set; }

        public ICollection<Car> Car { get; set; }
        public ICollection<CarRental> CarRental { get; set; }
    }
}
