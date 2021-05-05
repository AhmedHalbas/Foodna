using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class CategoryItem
    {
        [Key]
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
        public string PictureUri { get; set; }
        
        public int CategoryTypeId { get; set; }
        public virtual CategoryType CategoryType { get; set; }
    }
}
