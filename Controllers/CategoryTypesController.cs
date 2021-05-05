using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantProject.Models;
using RestaurantProject.Services;
using RestaurantProject.ViewModels;

namespace RestaurantProject.Controllers
{
    public class CategoryTypesController : Controller
    {
        private readonly ICategoryTypeRepoService categoryTypeRepoService;
        private readonly IRestaurantRepoService restaurantRepoService;
        public CategoryTypesController(ICategoryTypeRepoService _categoryTypeRepoService , IRestaurantRepoService _restaurantRepoService)
        {
            this.categoryTypeRepoService = _categoryTypeRepoService;
            this.restaurantRepoService = _restaurantRepoService;
        }

        // GET: CategoryTypes
        public async Task<IActionResult> Index()
        {
            return View(this.categoryTypeRepoService.GetAllCategoryTypes());
        }

        // GET: CategoryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryType = this.categoryTypeRepoService.GetDetails(id);
            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // GET: CategoryTypes/Create
        public IActionResult Create(int? id)
        {
            //RestaurantCategoryType curr = new RestaurantCategoryType()
            //{
            //    restaurant = this.restaurantRepoService.GetDetails(id),

            //};
            if (id == null)
            {
                return NotFound();
            }
            Restaurant curr = this.restaurantRepoService.GetDetails(id);
            if(curr!=null)
            {
                RestaurantCategoryType restaurantCategory = new RestaurantCategoryType()
                {
                    restaurant=curr
                    
            };
                //TempData["RestaurantID"] = curr;
                //TempData.Keep();
                return View(restaurantCategory);

            }
            else
            {
                return RedirectToAction("Details","Restaurants",new{id=id});
            }
        }

        // POST: CategoryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RestaurantCategoryType restaurantCategory)
        {
            //[Bind("categoryType.CatrgoryTypeID,categoryType.Type,restaurant.RestaurantID")]
             CategoryType categoryType = new CategoryType()
            {
                Type=restaurantCategory.categoryType.Type,
                RestaurantID=restaurantCategory.restaurant.RestaurantID,

            };
            
            if (this.restaurantRepoService.RestaurantExists(categoryType.RestaurantID)&& categoryType.Type!=null)
            {
                this.categoryTypeRepoService.Insert(categoryType);
                return RedirectToAction("Details","Restaurants",new { id= categoryType.RestaurantID });
            }
            return View(restaurantCategory);
        }

        // GET: CategoryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryType = this.categoryTypeRepoService.GetDetails(id);
            if (categoryType == null)
            {
                return NotFound();
            }
            return View(categoryType);
        }

        // POST: CategoryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatrgoryTypeID,Type,RestaurantID")] CategoryType categoryType)
        {
            if (id != categoryType.CatrgoryTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.categoryTypeRepoService.UpdateCategoryType(id, categoryType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryTypeExists(categoryType.CatrgoryTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","Restaurants",new { id=categoryType.RestaurantID});
            }
            return View(categoryType);
        }

        // GET: CategoryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryType = this.categoryTypeRepoService.GetDetails(id);
            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // POST: CategoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //context.CategoryTypes
            //    .Include(c => c.Restaurant)
            //    .FirstOrDefaultAsync(m => m.CatrgoryTypeID == id);
            int tempID = this.categoryTypeRepoService.GetDetails(id).RestaurantID;
            this.categoryTypeRepoService.DeleteCategoryType(id);
            return RedirectToAction("Details", "Restaurants",new { id=tempID});
        }

        private bool CategoryTypeExists(int id)
        {
            return this.categoryTypeRepoService.CategoryTypeExists(id);
        }
    }
}
