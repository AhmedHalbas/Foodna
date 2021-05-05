using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantProject.Models;

namespace RestaurantProject.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the RestaurantProjectUser class
    public class RestaurantProjectUser : IdentityUser
    {
        //public string Username { get; set; }

        public DateTime BirthDate { get; set; }
        public string PictureUri { get; set; }


        public  Address Address { set; get; }
    }
}
