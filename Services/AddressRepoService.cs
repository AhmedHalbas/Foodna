using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantProject.Services
{
    public class AddressRepoService : IAddressRepoService
    {
        private readonly myContext context;
        public AddressRepoService(myContext context)
        {
            this.context = context;
        }
        public bool AddressExists(int id)
        {
            return context.Addresses.Any(a=>a.AddressID == id);

        }

        public void DeleteAddress(int id)
        {
            context.Remove(context.Addresses.FirstOrDefault(a=>a.AddressID == id));
            context.SaveChanges();
        }

        public List<Address> GetAllAddresss()
        {
            return context.Addresses.ToList();
        }

        public Address GetDetails(int? id)
        {
            return context.Addresses.FirstOrDefault(a=>a.AddressID== id);

        }

        public void Insert(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        public void UpdateAddress(int id, Address address)
        {
            Address AddressUpdated = context.Addresses.FirstOrDefault(a=>a.AddressID == id);
            AddressUpdated.Street = address.Street;
            AddressUpdated.BuildingNom = address.BuildingNom;
            AddressUpdated.ApartmentNom = address.ApartmentNom;
            AddressUpdated.City = address.City;
            AddressUpdated.ZipCode = address.ZipCode;
            context.SaveChanges();
        }
    }
}
