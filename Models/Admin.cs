using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{

    public class Admin:BaseUser
    {
        [Required]
        public Roles Role { set; get; }
    }
}
