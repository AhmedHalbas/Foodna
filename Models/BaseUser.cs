using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class BaseUser
    {

        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string PictureUri { get; set; }

        
        
    }
   
}
