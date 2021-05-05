using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class CategoryType
    {
        [Key]
        public int CatrgoryTypeID { get; set; }
        [Required]
        public string Type { get; set; }
        public virtual Restaurant Restaurant { set; get; }
        [Required]
        public int RestaurantID { set; get; }
        public virtual ICollection<CategoryItem> CategoryItems { set; get; }
    }
}
