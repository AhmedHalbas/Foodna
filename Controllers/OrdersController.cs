using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Models;
using RestaurantProject.ViewModels;
using RestaurantProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantProject.Controllers
{
    [Authorize]

    public class OrdersController : Controller
    {
        private readonly IOrderRepoService orderRepoService;
        private readonly IBuyerRepoService buyerRepoService;
        private readonly ICategoryItemRepoService categoryItemRepoService;
        private readonly UserManager<RestaurantProjectUser> UserManager;


        public OrdersController(IOrderRepoService _orderRepoService ,IBuyerRepoService _buyerRepoService,
        UserManager<RestaurantProjectUser> _userManager , ICategoryItemRepoService _categoryItemRepoService)
         
        {
            this.orderRepoService = _orderRepoService;
            this.buyerRepoService = _buyerRepoService;
            this.UserManager = _userManager;
            this.categoryItemRepoService = _categoryItemRepoService;
        }

        [HttpPost]
        public IActionResult ApproveOrder(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Order order = this.orderRepoService.GetDetails(id);
            if (id != order.OrderID)
            {
                return NotFound();
            }
            order.orderStatus = OrderStatus.Approved;
            this.orderRepoService.UpdateOrder(id, order);
            return RedirectToAction("Index");
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? flag)
        {


            Buyer curr = this.buyerRepoService.GetBuyerByUsername(this.UserManager.GetUserName(User));

            OrderItemCategoryItem current = new OrderItemCategoryItem()
            {
                OrdersList = this.orderRepoService.GetAllOrdersOfUserID(curr.UserID),
                categoryItemList = new List<CategoryItem>(),
                AllOrdersForAdmins = this.orderRepoService.GetUnApprovedOrders()



            };
            if (flag == 1)
            {
                current.flag = flag;
                current.Message = "Check out Successfully";
            }
            else if (flag == 0)
            {
                current.flag = flag;
                current.Message = "Something went wrong please confirm order again";
            }
            current.BuyerID = curr.UserID;
            current.total = 0;
            foreach (Order order in current.OrdersList)
            {
                if (order.orderStatus == OrderStatus.Waiting || order.orderStatus == OrderStatus.WaitingPayed)
                {
                    if (flag != 1 || flag != 0)
                    {
                        current.flag = 2;



                    }



                }
                else
                {



                    foreach (var item in order.OrderItems)
                    {
                        current.total += item.Price * item.Units;
                        var ct = this.categoryItemRepoService.GetDetails(item.CategoryItemID);



                        item.ItemOrdered = ct;



                    }
                }
            }
            current.taxes = (14 * current.total) / 100;
            current.delivery = 20;
            current.subTotal = current.total;
            current.total += (current.taxes + current.delivery);



            foreach (Order order in current.AllOrdersForAdmins)
            {



                foreach (var item in order.OrderItems)
                {
                    var ct = this.categoryItemRepoService.GetDetails(item.CategoryItemID);



                    item.ItemOrdered = ct;
                }
            }



            return View(current);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = this.orderRepoService.GetDetails(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["BuyerID"] = new SelectList(this.buyerRepoService.GetAllBuyers(), "UserID", "PictureUri");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,BuyerID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.orderStatus = OrderStatus.Waiting;
                this.orderRepoService.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerID"] = new SelectList(this.buyerRepoService.GetAllBuyers(), "UserID", "PictureUri", order.BuyerID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = this.orderRepoService.GetDetails(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["BuyerID"] = new SelectList(this.buyerRepoService.GetAllBuyers(), "UserID", "PictureUri", order.BuyerID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,BuyerID,OrderDate")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.orderRepoService.UpdateOrder(id, order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            ViewData["BuyerID"] = new SelectList(this.buyerRepoService.GetAllBuyers(), "UserID", "PictureUri", order.BuyerID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = this.orderRepoService.GetDetails(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            this.orderRepoService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return this.orderRepoService.OrderExists(id);
        }
    }
}
