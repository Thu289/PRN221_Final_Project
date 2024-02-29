using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Repositories
{
    public class OrderCartImports : IOrderCartImport
    {
        Prn221ManageStoreContext _context;

        public OrderCartImports(Prn221ManageStoreContext ctx)
        {
            _context = ctx;
        }

        public void AddAccToCart(int cartId, int accId)
        {
            OrderCartImport cart = GetById(cartId);
            cart.AccId = accId;
            _context.OrderCartImports.Update(cart);
            _context.SaveChanges();
        }

        public void AddItemToCart(int cartId, OrderDetail orderDetail)
        {
            OrderCartImport cart = _context.OrderCartImports.Include(o=>o.OrderDetails).FirstOrDefault(o=>o.Id== cartId);
            OrderDetail oldOrderDetail = cart.OrderDetails.FirstOrDefault(o => o.ProductId == orderDetail.ProductId);
            if (cart.TotalPrice == null)
            {
                cart.TotalPrice = 0;
            }
            cart.TotalPrice += orderDetail.Price * orderDetail.Quantity;
            if (oldOrderDetail != null)
            {
                oldOrderDetail.Quantity += orderDetail.Quantity;
                _context.OrderDetails.Update(oldOrderDetail);
            }
            else
            {
                cart.OrderDetails.Add(orderDetail);
                _context.OrderCartImports.Update(cart);
            }
            _context.SaveChanges();
        }

        public void Checkout(int cartId)
        {
            OrderCartImport cart = GetById(cartId);
            cart.Type = "order";
            _context.OrderCartImports.Update(cart);
            _context.SaveChanges();
        }

        public OrderCartImport GetById(int id)
        {
            return _context.OrderCartImports.Include(o => o.OrderDetails).ThenInclude(o => o.Product).FirstOrDefault(o => o.Id == id);
        }

        public void MergeCartLogin(int cartCookie, int cartAcc)
        {
            OrderCartImport cartNotLogin = GetById(cartCookie);
            OrderCartImport cartLogin = GetById(cartAcc);
            foreach(OrderDetail orderDetail in cartNotLogin.OrderDetails)
            {
                OrderDetail o = cartLogin.OrderDetails.FirstOrDefault(o => o.ProductId == orderDetail.ProductId);
                if (o == null)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = cartLogin.Id;
                    od.ProductId = orderDetail.ProductId;
                    od.Quantity = orderDetail.Quantity;
                    od.Price = orderDetail.Price;
                    cartLogin.OrderDetails.Add(od);
                }
                else
                {
                    o.Quantity += orderDetail.Quantity;
                    _context.OrderDetails.Update(o);
                }
                _context.OrderCartImports.Update(cartLogin);
            }
            cartNotLogin.Status = "0";
            _context.OrderCartImports.Update(cartNotLogin);
            _context.SaveChanges();
        }

        public void RemoveItem(int cartId, int pid)
        {
            OrderCartImport cart = GetById(cartId);
            cart.OrderDetails.Remove(cart.OrderDetails.FirstOrDefault(o=>o.ProductId==pid));
            _context.Update(cart);
            _context.SaveChanges();
        }

        void IOrderCartImport.AddOrderCartImport(OrderCartImport orderCartImport)
        {
            orderCartImport.CreatedTime = DateTime.Now;
            orderCartImport.Status = "1";
           _context.OrderCartImports.Add(orderCartImport);
            _context.SaveChanges();
        }

        void IOrderCartImport.DeleteOrderCartImport(int id)
        {
            throw new NotImplementedException();
        }

        List<OrderCartImport> IOrderCartImport.GetAll(string type)
        {
            return _context.OrderCartImports.Where(o=>o.Type.ToLower()==type.ToLower()).ToList();
        }

        List<OrderCartImport> IOrderCartImport.GetAllOrderOfAcc(int accId)
        {
            return _context.OrderCartImports.Include(o=>o.Acc).Where(o=>o.Type=="order" && o.Acc.Id==accId).ToList();
        }

        OrderCartImport IOrderCartImport.GetCart(int accId)
        {
            return _context.OrderCartImports.Include(o=>o.OrderDetails).ThenInclude(o=>o.Product).FirstOrDefault(o => o.AccId == accId && o.Type == "cart" && o.Status == "1");
        }

        void IOrderCartImport.UpdateOrderCartImport(OrderCartImport orderCartImport)
        {
            _context.OrderCartImports.Update(orderCartImport);
            _context.SaveChanges();
        }
    }
}
