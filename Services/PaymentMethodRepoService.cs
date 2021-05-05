using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class PaymentMethodRepoService : IPaymentMethodRepoService
    {
        private readonly myContext context;
        public PaymentMethodRepoService(myContext context)
        {
            this.context = context;
        }
        public bool PaymentMethodExists(int id)
        {
            return context.PaymentMethods.Any(p=>p.PaymentMethodID == id);

        }

        public void DeletePaymentMethod(int id)
        {
            context.Remove(context.PaymentMethods.FirstOrDefault(p=>p.PaymentMethodID == id));
            context.SaveChanges();
        }

        public List<PaymentMethod> GetAllPaymentMethods()
        {
            return context.PaymentMethods.ToList();
        }

        public PaymentMethod GetDetails(int? id)
        {
            return context.PaymentMethods.FirstOrDefault(p=>p.PaymentMethodID == id);
        }

        public void Insert(PaymentMethod paymentMethod)
        {
            context.PaymentMethods.Add(paymentMethod);
            context.SaveChanges();
        }

        public void UpdatePaymentMethod(int id, PaymentMethod paymentMethod)
        {
            PaymentMethod PaymentMethodUpdated = context.PaymentMethods.FirstOrDefault(p=>p.PaymentMethodID == id);
            PaymentMethodUpdated.Alias = paymentMethod.Alias;
            PaymentMethodUpdated.CardId = paymentMethod.CardId;
            PaymentMethodUpdated.Last4 = paymentMethod.Last4;
            
            context.SaveChanges();
        }
    }
}
