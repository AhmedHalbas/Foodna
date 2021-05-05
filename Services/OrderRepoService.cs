using Microsoft.EntityFrameworkCore;
using RestaurantProject.Models;
using RestaurantProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class OrderRepoService:IOrderRepoService
    {
        private readonly myContext context;
        public OrderRepoService(myContext context)
        {
            this.context = context;
        }
        public void DeleteOrder(int id)
        {
            context.Remove(context.Orders.FirstOrDefault(r => r.OrderID == id));
            context.SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            return context.Orders.Include(o => o.OrderItems).ToList();
        }
        public List<Order> GetAllOrdersOfUserID(int? userID)
        {

            return context.Orders.Include(o => o.OrderItems)
                 .Where(o => o.BuyerID == userID && o.orderStatus != OrderStatus.Approved).ToList();
        }

        public Order GetDetails(int? id)
        {
            return context.Orders.Include(o => o.Buyer).FirstOrDefault(m => m.OrderID == id);
        }

        public int Insert(Order Order)
        {
            context.Orders.Add(Order);
            context.SaveChanges();
            return Order.OrderID;
        }

        public bool OrderExists(int id)
        {
            return context.Orders.Any(r => r.OrderID == id);
        }

        public void UpdateOrder(int id, Order order)
        {
            Order OrderUpdated = context.Orders.FirstOrDefault(o => o.OrderID == id);
            OrderUpdated.OrderDate = order.OrderDate;
            OrderUpdated.PaymentMethod = order.PaymentMethod;
            OrderUpdated.ShipToAddress = order.ShipToAddress;
            


            context.SaveChanges();
        }

        public List<Order> GetUnApprovedOrders()
        {
            return context.Orders.Include(o => o.OrderItems).Where(o => o.orderStatus == OrderStatus.Waiting || o.orderStatus == OrderStatus.WaitingPayed).ToList();
        }
    }
}
