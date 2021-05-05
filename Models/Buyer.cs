using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    public class Buyer:BaseUser
    {
       
        public PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

       

        public Address Address { set; get; }

        public string PhoneNumber { get; set; }
    }
}
