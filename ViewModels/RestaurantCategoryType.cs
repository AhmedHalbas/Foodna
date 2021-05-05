using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantProject.Models;

namespace RestaurantProject.ViewModels
{
    public class RestaurantCategoryType
    {
        public Restaurant restaurant { set; get; }
        public CategoryType categoryType { set; get; }
        public Restaurant cartRest { set; get; }
        public List<CategoryType> categoryTypes { set; get; }
        public List<CategoryItem> categoryItems { set; get; }
        public CategoryItemViewModel CategoryItemViewModel { set; get; }
        public string Flag { set; get; }
    }
}
