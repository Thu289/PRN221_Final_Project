using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class RoleModel : PageModel
    {

        Prn221ManageStoreContext _context;
        IRole _role;


        public List<Role> Roles { get; set; }

        public RoleModel(Prn221ManageStoreContext context, IRole role)
        {
            _context = context;
            _role = role;
        }
        public void OnGet()
        {
            Roles=_role.GetAllRoles();
        }
    }
}
