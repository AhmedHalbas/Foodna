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
using RestaurantProject.Services;
using RestaurantProject.ViewModels;
using Stripe;

namespace RestaurantProject.Controllers
{
    public class BuyersController : Controller
    {
        private readonly IBuyerRepoService buyerRepoService;
        private readonly IOrderRepoService orderRepoService;
        private readonly IOrderItemsRepoService orderItemsRepoService;
        private readonly UserManager<RestaurantProjectUser> UserManager;


        public BuyersController(IBuyerRepoService _buyerRepoService,
            IOrderRepoService _orderRepoService,
            IOrderItemsRepoService _orderItemsRepoService,
            UserManager<RestaurantProjectUser> _userManager)
        {
            this.buyerRepoService = _buyerRepoService;
            this.UserManager = _userManager;
            this.orderRepoService = _orderRepoService;
            this.orderItemsRepoService = _orderItemsRepoService;

        }

        // GET: Buyers
        public async Task<IActionResult> Index()
        {
            return View(this.buyerRepoService.GetAllBuyers());
        }

        // GET: Buyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = this.buyerRepoService.GetDetails(id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // GET: Buyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Username,BirthDate,PictureUri")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                this.buyerRepoService.Insert(buyer);
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        // GET: Buyers/Edit/5
       

        public async Task<IActionResult> ConfirmOrder(int? id, decimal? total)
        {
            ConfirmOrder confirm = new ConfirmOrder();
            if (id == null)
            {
                return NotFound();
            }

            var buyer = this.buyerRepoService.GetDetails(id);
            if (buyer == null)
            {
                return NotFound();
            }
            confirm.buyer = buyer;
            confirm.total = total*100;
            return View(confirm);

        }

        [HttpPost]
        public IActionResult Confirm(ConfirmOrder confirm)
        {
            List<Models.Order> OrdersList = this.orderRepoService.GetAllOrdersOfUserID(confirm.buyer.UserID);
            if (OrdersList != null)
            {
                foreach (var item in OrdersList)
                {
                    item.orderStatus = OrderStatus.Waiting;
                    this.orderRepoService.UpdateOrder(item.OrderID, item);

                }



                return RedirectToAction("Index", "Orders", new { flag = 1 });
            }
            else
            {
                return RedirectToAction("Index", "Orders", new { flag = 0 });
            }


        }
        [HttpPost]
        public IActionResult Charge(string stripeEmail, string stripeToken, ConfirmOrder confirm)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();



            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            long total = (long)confirm.total;
            var charge = charges.Create(new ChargeCreateOptions
            {

                Amount = total,
                Description = "Test payment",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,

            });
            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                //update order status
                List<Models.Order> OrdersList = this.orderRepoService.GetAllOrdersOfUserID(confirm.buyer.UserID);
                if (OrdersList != null)
                {
                    foreach (var item in OrdersList)
                    {
                        item.orderStatus = OrderStatus.WaitingPayed;

                        this.orderRepoService.UpdateOrder(item.OrderID, item);
                    }
                }
                return RedirectToAction("Index", "Orders", new { flag = 1 });
            }
            else
            {
                return RedirectToAction("Index", "Orders", new { flag = 0 });



            }



        }

        public async Task<IActionResult> Edit(int? id )
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = this.buyerRepoService.GetDetails(id);
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Username,BirthDate,PictureUri")] Buyer buyer)
        {
            if (id != buyer.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.buyerRepoService.UpdateBuyer(id, buyer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.UserID))
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
            return View(buyer);
        }
        

        // GET: Buyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = this.buyerRepoService.GetDetails(id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            this.buyerRepoService.DeleteBuyer(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BuyerExists(int id)
        {
            return this.buyerRepoService.BuyerExists(id);
        }
    }
}
