using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Data;

[assembly: HostingStartup(typeof(RestaurantProject.Areas.Identity.IdentityHostingStartup))]
namespace RestaurantProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RestaurantProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RestaurantProjectContextConnection")));

                //services.AddDefaultIdentity<RestaurantProjectUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<RestaurantProjectContext>();
            });
        }
    }
}