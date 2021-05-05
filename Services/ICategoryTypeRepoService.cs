using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantProject.Models;

namespace RestaurantProject.Services
{
    public interface ICategoryTypeRepoService
    {
        public int? GetRestaurantID(int? CategoryTypeID);
        public List<CategoryType> GetAllCategoryTypes();
        public List<CategoryType> GetAllCategoryTypesOfResID(int? resID);
        public CategoryType GetDetails(int? id);
        public void Insert(CategoryType buyer);
        public void UpdateCategoryType(int id, CategoryType buyer);
        public void DeleteCategoryType(int id);
        public bool CategoryTypeExists(int id);

    }
}
