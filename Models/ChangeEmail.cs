using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASSIGNMENT3.Models
{
    public class ChangeEmail
    {
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Wrong Email Fromat")]
        public string Email { get; set; }

    }
}
