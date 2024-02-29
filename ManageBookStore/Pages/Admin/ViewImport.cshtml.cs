using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class ViewImportModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IAccount _account;
        IOrderCartImport _orderCartImport;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public List<Account> ListSupplier { get; set; } = new List<Account>();

        public OrderCartImport Import { get; set; }=new OrderCartImport();

        public ViewImportModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories, IAccount account, IOrderCartImport orderCartImport)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
            _account = account;
            _orderCartImport = orderCartImport;
        }
        public void OnGet(int id)
        {
            Categories = _categories.GetAllCategories();
            Books = _bookRepository.GetAllBooks();
            ListSupplier = _account.GetAccountByRole("supplier");
            Import = _orderCartImport.GetById(id);
        }
    }
}
