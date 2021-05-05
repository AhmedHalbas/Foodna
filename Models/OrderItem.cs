using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public virtual CategoryItem ItemOrdered { get; set; }

        public int CategoryItemID { set; get; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10)]
        public int Units { get; set; }
        //public decimal Total { set; get; }
        public int OrderID { set; get; }
    }
}
