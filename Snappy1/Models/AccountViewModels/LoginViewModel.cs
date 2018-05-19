using System.ComponentModel.DataAnnotations;

namespace Snappy1.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        //[Required]
        //[Phone]
        //public string PhoneNumber { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
