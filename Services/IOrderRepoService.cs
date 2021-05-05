using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IOrderRepoService
    {
        public List<Order> GetAllOrders();
        public List<Order> GetAllOrdersOfUserID(int? userID);
        public List<Order> GetUnApprovedOrders();
        public Order GetDetails(int? id);
        public int Insert(Order Order);
        public void UpdateOrder(int id, Order Order);
        public void DeleteOrder(int id);
        public bool OrderExists(int id);
    }
}
