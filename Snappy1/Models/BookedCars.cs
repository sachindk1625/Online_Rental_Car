using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappy1.Models
{
    public class BookedCars
    {
        public int BookingId { get; set; }

        public DateTime BookingStartDate { get; set; }

        public DateTime BookingEndtDate { get; set; }

        public string CarModel { get; set; }

        public string CarBrand { get; set; }

        public int ProductionYear { get; set; }

        public string CarColor { get; set; }

        public string CategoryType { get; set; }

    }
}
