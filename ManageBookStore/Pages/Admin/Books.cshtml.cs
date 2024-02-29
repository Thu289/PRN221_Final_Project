using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using ManageBookStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    //[Authorize(Roles = "Admin")]
    public class BooksModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IAccount _account;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public BooksModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories, IAccount account)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
            _account = account;
        }


        public IActionResult OnGet()
        {
            string accId = HttpContext.Session.GetString("account");
            if (!string.IsNullOrEmpty(accId))
            {
                if(!_account.CheckRole(int.Parse(accId), "admin"))
                {
                    return Redirect("../login");
                }
            }
            else
            {
                return Redirect("../login");
            }
            Categories = _categories.GetAllCategories();
            Books = _bookRepository.GetAllBooks();
           return Page();

        }

        public void OnPost(int id)
        {

        }
    }
}
