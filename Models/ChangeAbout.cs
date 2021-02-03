using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASSIGNMENT3.Models
{
    public class ChangeAbout
    {
        [Required(ErrorMessage = "Please Write Something in About")]
        public string About { get; set; }

    }
}
