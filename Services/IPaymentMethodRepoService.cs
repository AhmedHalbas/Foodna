using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IPaymentMethodRepoService
    {
        public List<PaymentMethod> GetAllPaymentMethods();
        public PaymentMethod GetDetails(int? id);
        public void Insert(PaymentMethod paymentMethod);
        public void UpdatePaymentMethod(int id, PaymentMethod paymentMethod);
        public void DeletePaymentMethod(int id);
        public bool PaymentMethodExists(int id);
    }
}
