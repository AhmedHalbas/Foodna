using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Models
{
    
    public class PaymentMethod
    {
            [Key]
            public int PaymentMethodID { get; set; }
        
            [Required]
            public string Alias { get; set; }
            [Required]
            public string CardId { get; set; }
            [Required]
            [StringLength(4,ErrorMessage ="only 4 noms allowed")]
            public string Last4 { get; set; }
    }
    
}
