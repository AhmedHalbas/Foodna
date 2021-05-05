using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantProject.Services;
using Microsoft.EntityFrameworkCore;

namespace RestaurantProject.Services
{
    public class CategoryItemRepoService : ICategoryItemRepoService
    {
        private readonly myContext context;
        public CategoryItemRepoService(myContext context)
        {
            this.context = context;
        }


        public List<CategoryItem> GetAllCategoryItems()
        {
            return context.CategoryItems.Include(c => c.CategoryType).ToList();
        }
        public List<CategoryItem> Search(string name,int id)
        {
            return context.CategoryItems.Where(r => r.Name.Contains(name) && r.CategoryType.RestaurantID==id).ToList();
        }
        public void Insert(CategoryItem categoryItem)
        {
            context.CategoryItems.Add(categoryItem);
            context.SaveChanges();
        }

        public void UpdateCategoryItem(int id, CategoryItem categoryItem)
        {
            CategoryItem CategoryItemUpdated = context.CategoryItems.FirstOrDefault(c => c.CategoryItemID == id);
            CategoryItemUpdated.Name = categoryItem.Name;
            CategoryItemUpdated.Description = categoryItem.Description;
            CategoryItemUpdated.Price = categoryItem.Price;
            CategoryItemUpdated.PictureUri = categoryItem.PictureUri;
            CategoryItemUpdated.CategoryTypeId = categoryItem.CategoryTypeId;


            context.SaveChanges();
        }

        public void DeleteCategoryItem(int id)
        {
            context.Remove(context.CategoryItems.FirstOrDefault(c => c.CategoryItemID == id));
            context.SaveChanges();
        }

        public bool CategoryItemExists(int id)
        {
            return context.CategoryItems.Any(c => c.CategoryItemID == id);

        }

        public CategoryItem GetDetails(int? id)
        {
            return context.CategoryItems.
                Include(c => c.CategoryType).
                FirstOrDefault(c => c.CategoryItemID == id);

        }

        public List<CategoryItem> GetAllItemsByCategoryTypeID(int? id)
        {
            return context.CategoryItems.Where(ci => ci.CategoryTypeId == id).ToList();
        }
    }
}
