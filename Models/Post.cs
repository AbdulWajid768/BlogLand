using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASSIGNMENT3.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Tilte")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be 20")]
        [MinLength(5, ErrorMessage = "Minimum Length should be 5")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Write Something in Content")]
        public string Content { get; set; }



        public DateTime CurrentDateTime { get; set; }
    }
}
