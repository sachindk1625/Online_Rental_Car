using System.ComponentModel.DataAnnotations;

namespace Snappy1.Models
{
    public class UserInfoModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please enter License Number")]
        public string LicenseNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Please enter Email Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CarAvailabiltyModel CarAvailabiltyModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BookingModel BookingModel { get; set; }
    }
}