using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class Order
    {
        [Key]
        public int OrderID { set; get; }
        public virtual Buyer Buyer { get; set; }
        [Required]

        public int BuyerID { set; get; }
        [Required]

        public Address ShipToAddress { get;  set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public OrderStatus orderStatus { set; get; }

       
    }
}
