using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IReviewRepoService
    {
        public List<Review> GetAllReviews();
        public Review GetDetails(int? id);
        public void Insert(Review Review);
        public void UpdateReview(int id, Review Review);
        public void DeleteReview(int id);
        public bool ReviewExists(int id);
    }
}
