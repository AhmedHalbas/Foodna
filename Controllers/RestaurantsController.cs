using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Models;
using RestaurantProject.Services;
using RestaurantProject.ViewModels;
namespace RestaurantProject.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantRepoService restaurantRepository;
        private readonly ICategoryTypeRepoService categoryTypeRepository;
        private readonly ICategoryItemRepoService categoryItemRepository;
        private readonly IOrderRepoService orderRepoService;
        private readonly UserManager<RestaurantProjectUser> UserManager;
        private readonly IBuyerRepoService buyerRepoService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RestaurantsController(IRestaurantRepoService _restaurantRepository, ICategoryTypeRepoService _categoryTypeRepoService,
           ICategoryItemRepoService _categoryItemRepoService,
           IOrderRepoService _orderRepoService,
           UserManager<RestaurantProjectUser> _userManager,
           IBuyerRepoService _buyerRepoService, IWebHostEnvironment _webHostEnvironment)

        {
            this.restaurantRepository = _restaurantRepository;
            this.categoryTypeRepository = _categoryTypeRepoService;
            this.categoryItemRepository = _categoryItemRepoService;
            this.orderRepoService = _orderRepoService;
            this.UserManager = _userManager;
            this.buyerRepoService = _buyerRepoService;
            this.webHostEnvironment = _webHostEnvironment;

        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
                return View(this.restaurantRepository.GetAllRestaurants());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Name)
        {
            // Restaurant temp = this.restaurantRepository.SearchByName(Name);
            if (Name==null)
            {
                return View(this.restaurantRepository.GetAllRestaurants());
            }
             List<Restaurant> serResult = this.restaurantRepository.Search(Name);
            //if(temp==null)
            //{
            //    return View("NotFound");
            //}
            if (serResult == null)
            {
                return View("NotFound");
            }
            // ViewBag.SearchResult = temp;
            ViewBag.SearchResult = serResult;
            return View();
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var restaurant = this.restaurantRepository.GetDetails(id);
            if (restaurant == null)
            {
                return NotFound();

            }
            Buyer buyer = this.buyerRepoService.GetBuyerByUsername(this.UserManager.GetUserName(User));
            List<Order> cartOrders = new List<Order>();
            if (buyer != null)
            {
                cartOrders = this.orderRepoService.GetAllOrdersOfUserID(buyer.UserID);

            }

            Restaurant temp = new Restaurant();

            if (cartOrders.Count <= 0)
            {
                temp = restaurant;
            }
            else
            {
                foreach (var order in cartOrders)
                {
                    foreach (var item in order.OrderItems)
                    {
                        // current.total += item.Price * item.Units;
                        var ct = this.categoryItemRepository.GetDetails(item.CategoryItemID);
                        item.ItemOrdered = ct;
                        int? categoryTypeID = item.ItemOrdered.CategoryTypeId;
                        int? resID = this.categoryTypeRepository.GetRestaurantID(categoryTypeID);
                        if (restaurant.RestaurantID != resID)
                        {
                            temp = this.restaurantRepository.GetDetails(resID);
                        }
                        else
                        {
                            temp = restaurant;
                        }
                    }
                }
            }

            RestaurantCategoryType tempModel = new RestaurantCategoryType()
            {
                restaurant = restaurant,
                categoryTypes = this.categoryTypeRepository.GetAllCategoryTypesOfResID(id),
                cartRest = temp

            };
            tempModel.Flag = "flex";
            return View(tempModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id,string Name)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = this.restaurantRepository.GetDetails(id);
            if (restaurant == null)
            {
                return NotFound();

            }
            Buyer buyer = this.buyerRepoService.GetBuyerByUsername(this.UserManager.GetUserName(User));
            List<Order> cartOrders = new List<Order>();
            if (buyer != null)
            {
                cartOrders = this.orderRepoService.GetAllOrdersOfUserID(buyer.UserID);

            }

            Restaurant temp = new Restaurant();

            if (cartOrders.Count <= 0)
            {
                temp = restaurant;
            }
            else
            {
                foreach (var order in cartOrders)
                {
                    foreach (var item in order.OrderItems)
                    {
                        // current.total += item.Price * item.Units;
                        var ct = this.categoryItemRepository.GetDetails(item.CategoryItemID);
                        item.ItemOrdered = ct;
                        int? categoryTypeID = item.ItemOrdered.CategoryTypeId;
                        int? resID = this.categoryTypeRepository.GetRestaurantID(categoryTypeID);
                        if (restaurant.RestaurantID != resID)
                        {
                            temp = this.restaurantRepository.GetDetails(resID);
                        }
                        else
                        {
                            temp = restaurant;
                        }
                    }
                }
            }
            
            RestaurantCategoryType tempModel = new RestaurantCategoryType()
            {
                restaurant = restaurant,
                categoryTypes = this.categoryTypeRepository.GetAllCategoryTypesOfResID(id),
                cartRest = temp

            };
            if (Name == null)
            {
                tempModel.Flag = "flex";
                return View(tempModel);

            }
            List<CategoryItem> serResult = this.categoryItemRepository.Search(Name,id);
            if (serResult == null)
            {
                tempModel.Flag = "flex";
                return View("NotFound");
            }
            ViewBag.SearchResult = serResult;
            tempModel.Flag = "none";

            return View(tempModel);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        private Restaurant RestaurantWithUploadedFile(RestaurantViewModel model)
        {
            string uniqueFileName = null;

            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            if (model.Image != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                //uniqueFileName = model.Name+"-"+model.Describtion.Replace(" ", string.Empty) + Path.GetExtension(model.Image.FileName);
                uniqueFileName = "restaurant" + "-" + model.RestaurantID + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            }


            Restaurant restaurant = new Restaurant
            {
                Name = model.Name,
                Describtion = model.Describtion,
                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                PicUri = uniqueFileName,

            };

            return restaurant;
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {


                this.restaurantRepository.Insert(RestaurantWithUploadedFile(model));
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = this.restaurantRepository.GetDetails(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            RestaurantViewModel model = new RestaurantViewModel { RestaurantID = restaurant.RestaurantID, Name = restaurant.Name, Describtion = restaurant.Describtion };
            return View(model);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantViewModel model)
        {
            if (id != model.RestaurantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.restaurantRepository.UpdateRestaurant(id, RestaurantWithUploadedFile(model));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(model.RestaurantID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = this.restaurantRepository.GetDetails(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            this.restaurantRepository.DeleteRestaurant(id);
            return RedirectToAction("Index", "Restaurants");
        }

        private bool RestaurantExists(int id)
        {
            return this.restaurantRepository.RestaurantExists(id);
        }

    }
}
