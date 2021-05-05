using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class RestaurantRepoService : IRestaurantRepoService
    {
        private readonly myContext context;
        public RestaurantRepoService(myContext _context)
        {
            this.context = _context;
        }
        public Restaurant SearchByName(string name)
        {
            return context.Restaurants.FirstOrDefault(r => r.Name == name);
        }
        public List<Restaurant> Search(string name)
        {
            return context.Restaurants.Where(r => r.Name.Contains(name)).ToList();
        }
        public void DeleteRestaurant(int id)
        {
            context.Remove(context.Restaurants.FirstOrDefault(r => r.RestaurantID == id));
            context.SaveChanges();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return context.Restaurants.ToList();
        }

        public Restaurant GetDetails(int? id)
        {
            return context.Restaurants.FirstOrDefault(r => r.RestaurantID == id);
        }

        public void Insert(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
        }

        public bool RestaurantExists(int id)
        {
            return context.Restaurants.Any(r => r.RestaurantID == id);
        }

        public void UpdateRestaurant(int id, Restaurant restaurant)
        {
            Restaurant RestaurantUpdated = context.Restaurants.FirstOrDefault(r => r.RestaurantID == id);
            RestaurantUpdated.Name = restaurant.Name;
            RestaurantUpdated.Describtion = restaurant.Describtion;
            context.SaveChanges();
        }
    }
}
