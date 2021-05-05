using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantProject.Models;

namespace RestaurantProject.ViewModels
{
    public class OrderItemCategoryItem
    {
        public List<Order> AllOrdersForAdmins { set; get; }
        public decimal taxes { set; get; }
        public decimal subTotal { set; get; }
        public decimal delivery { set; get; }
        public int BuyerID { set; get; }
        public string Message { set; get; }
        public int? flag { set; get; }
        public OrderItem orderItem { set; get; }
        public CategoryItem categoryItem { set; get; }
        public List<Order> OrdersList { set; get; }
        public List<CategoryItem> categoryItemList { set; get; }
        public decimal total { set; get; }
        public int? ResID { set; get; }
    }
}
