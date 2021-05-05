using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class Address
    {
        [Key]
        public int AddressID { set; get; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public int BuildingNom { set; get; }

        public int ApartmentNom { set; get; }
    }
}
