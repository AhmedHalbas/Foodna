using Microsoft.EntityFrameworkCore;
using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class BuyerRepoService:IBuyerRepoService
    {
        private readonly myContext context;
        public BuyerRepoService(myContext context)
        {
            this.context = context;
        }
        public List<Buyer> GetAllBuyers()
        {
            return context.Buyers.ToList();
        }

        public Buyer GetBuyerByUsername (string buyerUsername)
        {
            return context.Buyers.FirstOrDefault(b => b.Username == buyerUsername);
        }
        public Buyer GetDetails(int? id)
        {
            return context.Buyers.Include(b=>b.Address).FirstOrDefault(b => b.UserID == id);
        }

        public void Insert(Buyer Buyer)
        {
            context.Buyers.Add(Buyer);
            context.SaveChanges();
        }

        

        
        public bool BuyerExists(int id)
        {
            return context.Buyers.Any(e => e.UserID== id);
        }

        public void UpdateBuyer(int id, Buyer buyer)
        {
            Buyer BuyerUpdated = context.Buyers.Find(id);
            BuyerUpdated.Address = buyer.Address;
            BuyerUpdated.BirthDate = buyer.BirthDate;
            BuyerUpdated.Orders = buyer.Orders;
            BuyerUpdated.PictureUri = buyer.PictureUri;
            BuyerUpdated.PaymentMethod = buyer.PaymentMethod;
            BuyerUpdated.Username = buyer.Username;


            context.SaveChanges();

        }

        public void DeleteBuyer(int id)
        {
            context.Remove(context.Buyers.FirstOrDefault(b => b.UserID == id));
            context.SaveChanges();
        }
    }
}
