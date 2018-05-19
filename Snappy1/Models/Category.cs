using System;
using System.Collections.Generic;

namespace Snappy1.Models
{
    public partial class Category
    {
        public Category()
        {
            Car = new HashSet<Car>();
        }

        public string CategoryType { get; set; }
        public decimal Amount { get; set; }
        public string ImageLoc { get; set; }

        public ICollection<Car> Car { get; set; }
    }
}
