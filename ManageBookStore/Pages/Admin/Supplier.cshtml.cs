using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class SupplierModel : PageModel
    {

        Prn221ManageStoreContext _context;
        IAccount _account;

        public List<Account> AccountList { get; set; } = new List<Account>();
        public SupplierModel(Prn221ManageStoreContext context, IAccount account)
        {
            _context = context;
            _account = account;
        }

        public void OnGet()
        {
            AccountList = _account.GetAccountByRole("supplier");
        }
    }
}
