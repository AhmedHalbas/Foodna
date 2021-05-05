using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.ViewModels
{
    public class CategoryItemViewModel
    {
        public int CategoryItemID { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public int CategoryTypeId { get; set; }
    }
}
