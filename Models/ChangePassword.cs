using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASSIGNMENT3.Models
{
    public class ChangePassword
    {
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Password can only be Alphanumeric")]
        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be 20")]
        [MinLength(5, ErrorMessage = "Minimum Length should be 5")]
        public string Password { get; set; }

    }
}
