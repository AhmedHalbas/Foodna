using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Data;
using RestaurantProject.Models;
using RestaurantProject.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<RestaurantProjectUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RestaurantProjectContext>();



            services.AddAuthentication().AddFacebook(
                facebookOptions =>
            {
                facebookOptions.AppId = "1120653528437402";
                facebookOptions.AppSecret = "1548ea0b1020e5df0f2b63b6c77de55c";
            });
            services.AddAuthentication().AddGoogle(options =>
            {

                options.ClientId = "192953861334-ei66q8pnscblpu2rqo4ketcs7q7e6k7o.apps.googleusercontent.com";
                options.ClientSecret = "pF2Wcq_fEIy0jepwcCHLRx2k";
            });

            services.AddAuthentication();
            services.AddAuthorization();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddScoped<IBuyerRepoService, BuyerRepoService>();
            services.AddScoped<IAdminRepoService, AdminRepoService>();
            services.AddScoped<IRestaurantRepoService, RestaurantRepoService>();
            services.AddScoped<IOrderRepoService, OrderRepoService>();
            services.AddScoped<IOrderItemsRepoService, OrderItemRepoService>();
            services.AddScoped<ICategoryTypeRepoService, CategoryTypeRepoService>();
            services.AddScoped<IReviewRepoService, ReviewRepoService>();
            services.AddScoped<IAddressRepoService, AddressRepoService>();
            services.AddScoped<IPaymentMethodRepoService, PaymentMethodRepoService>();
            services.AddScoped<ICategoryItemRepoService, CategoryItemRepoService>();


            services.AddDbContext<myContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("myConn"))
           );

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });

        }
    }
}
