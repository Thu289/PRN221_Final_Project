using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages
{
    public class RegisterModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IAccount _account;


        public RegisterModel(Prn221ManageStoreContext context, IAccount account)
        {
            _context = context;
            _account = account;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string RePass { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Addresses { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_account.CheckEmailExist(Email) == true || _account.CheckUserNameExist(UserName) == true)
            {
                ViewData["Error"] = "This username/ email has been in system!";
                return Page();
            }
            else if (Password != RePass)
            {
                ViewData["Error"] = "Password doesn't match re password!";
                return Page();
            }
            Account account = new Account();
            account.UserName = UserName;
            account.Password = Password;
            account.Email = Email;
            account.Phone = Phone;
            account.FullName = FullName;
            account.IsActive = true;
            if (Addresses != null && Addresses.Length > 0)
            {
                Address a = new Address();
                a.Address1 = Addresses;
                a.IsDefault = true;
                account.Addresses.Add(a);
            }
            _account.AddAccount(account);
            _account.AddRole(account.Id, "user");
            return Redirect("login");
        }
    }
}
