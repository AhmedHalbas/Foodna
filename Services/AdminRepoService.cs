using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class AdminRepoService : IAdminRepoService
    {
        private readonly myContext context;
        public AdminRepoService(myContext context)
        {
            this.context = context;
        }
        public void DeleteAdmin(int id)
        {
            context.Remove(context.Admins.FirstOrDefault(a =>a.UserID == id));
            context.SaveChanges();
        }

        public List<Admin> GetAllAdmins()
        {
            return context.Admins.ToList();

        }

        public Admin GetDetails(int? id)
        {
            return context.Admins.FirstOrDefault(a=>a.UserID == id);

        }

        public void Insert(Admin admin)
        {
            context.Admins.Add(admin);
            context.SaveChanges(); 
        }

        public bool AdminExists(int id)
        {
            return context.Admins.Any(a=>a.UserID == id);

        }

        public void UpdateAdmin(int id, Admin admin)
        {
            Admin AdminUpdated = context.Admins.FirstOrDefault(a => a.UserID == id);

            AdminUpdated.BirthDate = admin.BirthDate;
            AdminUpdated.PictureUri = admin.PictureUri;
            AdminUpdated.Username = admin.Username;
            AdminUpdated.Role = admin.Role;

            context.SaveChanges();
        }
    }
}
