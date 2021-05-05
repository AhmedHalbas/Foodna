using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IRestaurantRepoService
    {
        public List<Restaurant> Search(string name);
        public List<Restaurant> GetAllRestaurants();
        public Restaurant GetDetails(int? id);
        public Restaurant SearchByName(string name);
        public void Insert(Restaurant restaurant);
        public void UpdateRestaurant(int id, Restaurant restaurant);
        public void DeleteRestaurant(int id);
        public bool RestaurantExists(int id);
    }

}
