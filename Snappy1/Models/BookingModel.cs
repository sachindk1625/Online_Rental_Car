using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snappy1.Models
{
    public class BookingModel
    {
        public BookingModel()
        {
            AllLocations = new List<SelectListItem>();
        }
        /// <summary>
        /// 
        /// </summary>

        [Required(ErrorMessage = "Please enter Start Date")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please enter End Date")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please select location")]
        public string Location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ReturnDifferentLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectListItem> AllLocations { get; set; }
    }
}
