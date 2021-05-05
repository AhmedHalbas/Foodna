using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class ReviewRepoService: IReviewRepoService
    {
        private readonly myContext context;
        public ReviewRepoService(myContext context)
        {
            this.context = context;
        }
        public List<Review> GetAllReviews()
        {
            return context.Reviews.ToList();
        }

        public Review GetDetails(int? id)
        {
            return context.Reviews.FirstOrDefault(r => r.ReviewID == id);
        }

        public void Insert(Review Review)
        {
            context.Reviews.Add(Review);
            context.SaveChanges();
        }




        public bool ReviewExists(int id)
        {
            return context.Reviews.Any(r => r.ReviewID == id);
        }

        public void UpdateReview(int id, Review review)
        {
            Review ReviewUpdated = context.Reviews.FirstOrDefault(r => r.ReviewID==id);
            ReviewUpdated.Rating = review.Rating;
            ReviewUpdated.Comment = review.Comment;
            
            context.SaveChanges();

        }

        public void DeleteReview(int id)
        {
            context.Remove(context.Reviews.FirstOrDefault(r=>r.ReviewID == id));
            context.SaveChanges();
        }
    }
}
