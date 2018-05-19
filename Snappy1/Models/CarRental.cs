using System;
using System.Collections.Generic;

namespace Snappy1.Models
{
    public partial class CarRental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public string Username { get; set; }
        public string LocationName { get; set; }
        public string Pickup { get; set; }
        public string Dropoff { get; set; }
        public DateTime BookingStartdate { get; set; }
        public DateTime BookingEnddate { get; set; }

        public Car Car { get; set; }
        public Location LocationNameNavigation { get; set; }
        public Customer UsernameNavigation { get; set; }
    }
}
