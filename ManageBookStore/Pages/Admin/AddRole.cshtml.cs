using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class AddRoleModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IRole _role;

        public AddRoleModel(Prn221ManageStoreContext context, IRole role)
        {
            _context = context;
            _role = role;
        }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "description")]
        public string Description { get; set; }

        [BindProperty(Name = "status")]
        public bool Status { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Role r = new Role();
            r.Name = Name;
            r.Description = Description;    
            r.Status = Status;
            _role.AddRole(r);
            return Redirect("role");
        }
    }
}
