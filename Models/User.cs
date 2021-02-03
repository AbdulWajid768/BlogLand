using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASSIGNMENT3.Models
{
    public class User
    {
        public int UserID { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Username can only be Alphanumeric")]
        [Required(ErrorMessage = "Please Enter Username")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be 20")]
        [MinLength(5, ErrorMessage = "Minimum Length should be 5")]
        public string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Password can only be Alphanumeric")]
        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be 20")]
        [MinLength(5, ErrorMessage = "Minimum Length should be 5")]
        public string Password { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Password can only be Alphanumeric")]
        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be 20")]
        [MinLength(5, ErrorMessage = "Minimum Length should be 5")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Wrong Email Fromat")]
        public string Email { get; set; }

       
        public string About { get; set; }
    }
}
