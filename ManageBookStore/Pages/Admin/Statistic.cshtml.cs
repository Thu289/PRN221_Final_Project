using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using ManageBookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Pages.Admin
{
    public class StatisticModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IOrderCartImport _orderCartImport;
        IBook _book;
        IOrderDetails _orderDetails;

        public List<Book> Books { get; set; } = new List<Book>();

        public StatisticModel(Prn221ManageStoreContext context, IOrderCartImport orderCartImport,IBook book, IOrderDetails orderDetails)
        {
            _context = context;
            _orderCartImport = orderCartImport;
            _book = book;
            _orderDetails = orderDetails;
        }
    
        public void OnGet()
        {
            ViewData["totalOrder"] = _context.OrderCartImports.Count(o => o.Type.ToLower() == "order");
            ViewData["totalPriceOrder"] = _context.OrderCartImports.OrderBy(o => o.Type.ToLower() == "order").Sum(o => o.TotalPrice);
            Books = _book.GetAllBooks();
            List<OrderDetail> listOrderDetail = _context.OrderDetails.Include(o => o.Order).Where(o => o.Order.Type.ToLower() == "order").ToList();
            foreach (var book in Books)
            {
                ViewData[book.Id.ToString()] = listOrderDetail.Where(o => o.ProductId == book.Id).Sum(o => o.Quantity * o.Price);
            }
        }
    }
}
