using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public interface IAddressRepoService
    {
        public List<Address> GetAllAddresss();
        public Address GetDetails(int? id);
        public void Insert(Address address);
        public void UpdateAddress(int id, Address address);
        public void DeleteAddress(int id);
        public bool AddressExists(int id);
    }
}
