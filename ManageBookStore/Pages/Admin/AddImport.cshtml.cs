using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class AddImportModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IAccount _account;
        IOrderCartImport _orderCartImport;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public List<Account> ListSupplier { get; set; } = new List<Account>();

        public AddImportModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories, IAccount account, IOrderCartImport orderCartImport)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
            _account = account;
            _orderCartImport = orderCartImport;
        }
        public void OnGet()
        {
            Categories = _categories.GetAllCategories();
            Books = _bookRepository.GetAllBooks();
            ListSupplier = _account.GetAccountByRole("supplier");
        }
        public IActionResult OnPost()
        {
            Books = _bookRepository.GetAllBooks();
            OrderCartImport newInport = new OrderCartImport();
            newInport.Status = "1";
            newInport.Type = "import";
            newInport.TotalPrice = 0;
            newInport.AccId = int.Parse(Request.Form["supplier"]);
            foreach (Book b in Books)
            {
                OrderDetail od = new OrderDetail();
                od.ProductId = b.Id;
                od.Price = b.Price;
                try
                {
                    od.Quantity = int.Parse(Request.Form[b.Id.ToString()]);
                }
                catch (Exception ex)
                {
                    od.Quantity = 0;
                }
                if (od.Quantity > 0 )
                {
                    newInport.OrderDetails.Add(od);
                    newInport.TotalPrice += od.Price * od.Quantity;
                    b.Quantity += od.Quantity;
                    _bookRepository.UpdateBook(b);
                }
            }
            _orderCartImport.AddOrderCartImport(newInport);
            return Redirect("import");
        }
    }
}
