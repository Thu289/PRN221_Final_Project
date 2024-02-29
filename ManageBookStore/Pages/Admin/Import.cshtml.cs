using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class ImportModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IOrderCartImport _orderCartImport;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public List<OrderCartImport> ListImport { get; set; } = new List<OrderCartImport>();    

        public ImportModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories, IOrderCartImport orderCartImport)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
            _orderCartImport = orderCartImport;
        }
        public void OnGet()
        {
            ListImport = _context.OrderCartImports.Where(o=>o.Type.ToLower()=="import").ToList();
        }
    }
}
