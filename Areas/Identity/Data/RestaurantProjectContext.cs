using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantProject.Areas.Identity.Data;
using RestaurantProject.Models;

namespace RestaurantProject.Data
{
    public class RestaurantProjectContext : IdentityDbContext<RestaurantProjectUser>
    {
        public RestaurantProjectContext(DbContextOptions<RestaurantProjectContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
