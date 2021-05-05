using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IAdminRepoService
    {
        public List<Admin> GetAllAdmins();
        public Admin GetDetails(int? id);
        public void Insert(Admin Admin);
        public void UpdateAdmin(int id, Admin Admin);
        public void DeleteAdmin(int id);
        public bool AdminExists(int id);
    }
}
