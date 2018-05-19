using System;
using System.Collections.Generic;

namespace Snappy1.Models
{
    public partial class Car
    {
        public Car()
        {
            CarRental = new HashSet<CarRental>();
        }

        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public string CategoryType { get; set; }
        public string LocationName { get; set; }
        public string ManagerUsername { get; set; }

        public Category CategoryTypeNavigation { get; set; }
        public Location LocationNameNavigation { get; set; }
        public Customer ManagerUsernameNavigation { get; set; }
        public ICollection<CarRental> CarRental { get; set; }
    }
}
