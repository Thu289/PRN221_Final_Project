using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IOrderCartImport
    {
        List<OrderCartImport> GetAll(string type);

        //List<OrderCartImport> GetByAccount(int accId);

        //List<OrderCartImport> GetByType(string type);

        List<OrderCartImport> GetAllOrderOfAcc(int accId);

        OrderCartImport GetCart(int accId);

        void AddOrderCartImport(OrderCartImport orderCartImport);

        void UpdateOrderCartImport(OrderCartImport orderCartImport);

        void DeleteOrderCartImport(int id);

        void AddItemToCart(int cartId, OrderDetail orderDetail);

        void MergeCartLogin(int cartCookie, int cartAcc);

        OrderCartImport GetById(int id);

        void RemoveItem(int cartId, int pid);

        void AddAccToCart(int cartId, int accId);

        void Checkout(int cartId);
    }
}
