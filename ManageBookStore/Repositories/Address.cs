using ManageBookStore.IRepositories;
using ManageBookStore.Models;

namespace ManageBookStore.Repositories
{
    public class Address : IAddress
    {
        Prn221ManageStoreContext _context;

        public Address(Prn221ManageStoreContext context)
        {
            _context = context;
        }

        void IAddress.AddAddress(Models.Address address)
        {
            _context.Addresses.Add(address);    
            _context.SaveChanges();
        }

        void IAddress.DeleteAddress(int addressId)
        {
            throw new NotImplementedException();
        }

        Models.Address IAddress.GetAddressDefault(int accId)
        {
            return _context.Addresses.FirstOrDefault(a => a.AccId == accId && a.IsDefault == true);
        }

        List<Models.Address> IAddress.GetAllAddresses(int accId)
        {
            return _context.Addresses.Where(a=>a.AccId== accId).ToList();
        }

        void IAddress.UpdateAddress(Models.Address address)
        {
            _context.Addresses.Update(address);
            _context.SaveChanges(); 
        }
    }
}
