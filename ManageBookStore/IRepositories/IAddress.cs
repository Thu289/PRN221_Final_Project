using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IAddress
    {
        List<Address> GetAllAddresses(int accId);

        Address GetAddressDefault(int accId);

        void AddAddress(Address address);

        void UpdateAddress(Address address);

        void DeleteAddress(int addressId);


    }
}
