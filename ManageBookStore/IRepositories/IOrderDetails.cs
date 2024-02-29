using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IOrderDetails
    {
        void AddOrderDetails(OrderDetail orderDetails);

        void UpdateOrderDetails(OrderDetail orderDetails);

        void DeleteOrderDetails(int orderDetailsId);

        List<OrderDetail> GetAllOrderDetails(int id);
    }
}
