using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Models;
using RestaurantProject.Services;

namespace RestaurantProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<RestaurantProjectUser> _userManager;
        private readonly IBuyerRepoService _buyerRepoService;

        public ConfirmEmailModel(UserManager<RestaurantProjectUser> userManager , IBuyerRepoService buyerRepoService )
        {
            _userManager = userManager;
            this._buyerRepoService = buyerRepoService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            //add to second database
            Buyer buyer = new Buyer()
            {
                BirthDate = user.BirthDate,
                Username = user.UserName,
                PictureUri =user.PictureUri,
                Address=user.Address
            
            };

            this._buyerRepoService.Insert(buyer);


            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return RedirectToPage("/Account/Login", new { area = "Identity" });
            //return RedirectToAction("Index");
        }
    }
}
