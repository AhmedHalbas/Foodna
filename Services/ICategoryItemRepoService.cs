using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface ICategoryItemRepoService
    {
        public List<CategoryItem> GetAllCategoryItems();
        public List<CategoryItem> GetAllItemsByCategoryTypeID(int? id);
        public CategoryItem GetDetails(int? id);
        public List<CategoryItem> Search(string name,int id);
        public void Insert(CategoryItem categoryItem);
        public void UpdateCategoryItem(int id, CategoryItem categoryItem);
        public void DeleteCategoryItem(int id);
        public bool CategoryItemExists(int id);
    }
}
