using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using ManageBookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace ManageBookStore.Pages.Admin
{
    public class AddCategoryModel : PageModel
    {
        Prn221ManageStoreContext _context;
        ICategories _categories;


        public AddCategoryModel(Prn221ManageStoreContext context, ICategories categories)
        {
            _context = context;
            _categories = categories;
        }

        [BindProperty(Name = "title")]
        public string Name { get; set; }

        [BindProperty(Name = "code")]
        public string Code { get; set; }

        [BindProperty(Name = "description")]
        public string Description { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (_categories.CheckNameCatExist(Name) == true)
            {
                ViewData["Error"] = "This categories has been in system!";
                return Page();
            }
            else
            {
                Category c = new Category();
                c.Name = Name;
                c.Code = Code;
                c.Description = Description;
                _categories.AddCategory(c);
                return Redirect("category");
            }
        }
    }
}
