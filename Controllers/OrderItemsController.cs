using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemsRepoService orderItemsRepoService;
        private readonly ICategoryItemRepoService categoryItemsRepoService;
        private readonly ICategoryTypeRepoService categoryTypeRepoService;
        private readonly IOrderRepoService orderRepoService;
        private readonly IBuyerRepoService buyerRepoService;
        private readonly UserManager<RestaurantProjectUser> UserManager;


        public OrderItemsController(IOrderItemsRepoService _orderItemsRepoService,
            ICategoryItemRepoService _categoryItemsRepoService,
            ICategoryTypeRepoService _categoryTypeRepoService,
            IOrderRepoService _orderRepoService,
            IBuyerRepoService _buyerRepoService,
            UserManager<RestaurantProjectUser> _userManager)
        {
            this.orderItemsRepoService = _orderItemsRepoService;
            this.categoryItemsRepoService = _categoryItemsRepoService;
            this.categoryTypeRepoService = _categoryTypeRepoService;
            this.orderRepoService = _orderRepoService;
            this.buyerRepoService = _buyerRepoService;
            this.UserManager = _userManager;
            
        }

        // GET: OrderItems
        
        public async Task<IActionResult> Index()
        {
            
            return View(this.orderItemsRepoService.GetAllOrderItemss());
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = this.orderItemsRepoService.GetDetails(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/Create
        [Authorize]
        public IActionResult Create(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            CategoryItem ct = this.categoryItemsRepoService.GetDetails(id);
            if (ct != null)
            {
                
                OrderItemCategoryItem current = new OrderItemCategoryItem()
                {
                    categoryItem = ct,
                    orderItem=new OrderItem(),
                    ResID= this.categoryTypeRepoService.GetRestaurantID(ct.CategoryTypeId)



                };
                current.orderItem.Units = 0;
                return View(current);
            }
            else
            {
                return RedirectToAction("Details", "Restaurants", new { id = this.categoryTypeRepoService.GetRestaurantID(id) });
            }
            //CategoryItem current = this.categoryItemsRepoService.GetDetails(categoryItemID);
           // ViewData["CategoryItemID"] = new SelectList(this.categoryItemsRepoService.GetAllCategoryItems(), "CategoryItemID", "Description");
            // ViewData["CategoryItemID"] = current;
            //TempData["CategoryItemID"] = current;
            //TempData.Peek("CategoryItemID")
           // return View(current);
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemID,CategoryItemID,Price,Units")] OrderItem orderItem)
        {
            Order order = new Order();
            if (orderItem.Units < 1)
            {
                ModelState.AddModelError("units", "please choose unit");
            }
            CategoryItem ct = this.categoryItemsRepoService.GetDetails(orderItem.CategoryItemID);
            OrderItemCategoryItem temp = new OrderItemCategoryItem()
            {
                categoryItem = ct,
                orderItem = orderItem,
                ResID = this.categoryTypeRepoService.GetRestaurantID(ct.CategoryTypeId)
            };
            if (ModelState.IsValid)
            {
                Buyer currentBuyer = this.buyerRepoService.GetBuyerByUsername(this.UserManager.GetUserName(User));
                order.OrderID = 0;
                order.BuyerID = currentBuyer.UserID;
                order.OrderDate = DateTime.Now;



                order.orderStatus = OrderStatus.Initial;
                int orderID = this.orderRepoService.Insert(order);
                orderItem.OrderID = orderID;
                this.orderItemsRepoService.Insert(orderItem);

                int? resID = this.categoryTypeRepoService.GetRestaurantID(this.categoryItemsRepoService.GetDetails(orderItem.CategoryItemID).CategoryTypeId);
                return RedirectToAction("Details", "Restaurants", new { id = resID });



            }
            return View(temp);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = this.orderItemsRepoService.GetDetails(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryItemID"] = new SelectList(this.categoryItemsRepoService.GetAllCategoryItems(), "CategoryItemID", "Description", orderItem.CategoryItemID);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderItemID,CategoryItemID,Price,Units")] OrderItem orderItem)
        {
            if (id != orderItem.OrderItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.orderItemsRepoService.UpdateOrderItems(id, orderItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OrderItemID))
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
            ViewData["CategoryItemID"] = new SelectList(this.categoryItemsRepoService.GetAllCategoryItems(), "CategoryItemID", "Description", orderItem.CategoryItemID);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = this.orderItemsRepoService.GetDetails(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            OrderItem orderitem = this.orderItemsRepoService.GetDetails(id);
            this.orderItemsRepoService.DeleteOrderItems(id);
            
            this.orderRepoService.DeleteOrder(orderitem.OrderID);
            return RedirectToAction("Index","Orders");
        }

        private bool OrderItemExists(int id)
        {
            return this.orderItemsRepoService.OrderItemsExists(id);
        }
    }
}
