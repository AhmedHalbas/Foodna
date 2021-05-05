using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IOrderItemsRepoService
    {
        public List<OrderItem> GetAllOrderItemss();
        public OrderItem GetDetails(int? id);
        public void Insert(OrderItem orderItem);
        public void UpdateOrderItems(int id, OrderItem orderItems);
        public void DeleteOrderItems(int id);
        public bool OrderItemsExists(int id);

    }
}
