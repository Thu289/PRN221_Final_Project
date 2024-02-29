using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{

    public class CategoryModel : PageModel
    {

        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public CategoryModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
        }

        public void OnGet()
        {
            Categories = _categories.GetAllCategories();
        }
    }
}
