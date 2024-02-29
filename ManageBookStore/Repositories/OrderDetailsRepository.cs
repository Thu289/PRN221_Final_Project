using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Repositories
{
    public class OrderDetailsRepository : IOrderDetails
    {
        Prn221ManageStoreContext _contect;

        public OrderDetailsRepository(Prn221ManageStoreContext context) {
            _contect = context;
        }

        public void AddOrderDetails(OrderDetail orderDetails)
        {
            _contect.OrderDetails.Add(orderDetails);
            _contect.SaveChanges();
        }

        public void UpdateOrderDetails(OrderDetail orderDetails)
        {
            _contect.OrderDetails.Update(orderDetails);
            _contect.SaveChanges();
        }

        void IOrderDetails.DeleteOrderDetails(int orderDetailsId)
        {
            throw new NotImplementedException();
        }

        List<OrderDetail> IOrderDetails.GetAllOrderDetails(int id)
        {
            return _contect.OrderDetails.Include(o=>o.Product).Where(o=>o.OrderId == id).ToList();
        }
    }
}
