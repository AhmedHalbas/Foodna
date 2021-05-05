using RestaurantProject.Models;
using System;
using RestaurantProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantProject.Services
{
    public class OrderItemRepoService: IOrderItemsRepoService
    {
        private readonly myContext context;
        public OrderItemRepoService(myContext context)
        {
            this.context = context;
        }
      

        public void DeleteOrderItems(int id)
        {
            context.Remove(context.OrderItems.FirstOrDefault(r => r.OrderItemID == id));
            context.SaveChanges();
        }

        public List<OrderItem> GetAllOrderItemss()
        {

            return context.OrderItems.Include(o => o.ItemOrdered).ToList();
        }

        public OrderItem GetDetails(int? id)
        {
            return context.OrderItems
                .Include(o => o.ItemOrdered)
                .FirstOrDefault(m => m.OrderItemID == id);
        }

        public void Insert(OrderItem OrderItem)
        {
            context.OrderItems.Add(OrderItem);
            context.SaveChanges();
        }
        public bool OrderItemsExists(int id)
        {
            return context.OrderItems.Any(r => r.OrderItemID == id);

        }

        public void UpdateOrderItems(int id, OrderItem orderItems)
        {
            OrderItem OrderItemUpdated = context.OrderItems.FirstOrDefault(o => o.OrderItemID == id);
            OrderItemUpdated.Units = orderItems.Units;
            OrderItemUpdated.Price = orderItems.Price;
            OrderItemUpdated.ItemOrdered = orderItems.ItemOrdered;
            OrderItemUpdated.CategoryItemID = orderItems.CategoryItemID;
            OrderItemUpdated.OrderItemID = orderItems.OrderItemID;
            context.SaveChanges();
        }
    }
}
