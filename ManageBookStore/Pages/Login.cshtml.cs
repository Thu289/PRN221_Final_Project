using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using ManageBookStore.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ManageBookStore.Pages
{
    public class LoginModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IAccount _account;
        IOrderCartImport _orderCartImport;


        public LoginModel(Prn221ManageStoreContext context, IAccount account, IOrderCartImport orderCartImport)
        {
            _context = context;
            _account = account;
            _orderCartImport = orderCartImport;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Account a = _account.GetAccountByLogin(UserName, Password);
            if (a == null)
            {
                ViewData["error"] = "Username (or email) and password is not match any account!";
                return Page();
            }
            HttpContext.Session.SetString("account", a.Id.ToString());

            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                OrderCartImport cartAcc = _orderCartImport.GetCart(a.Id);
                if (cartAcc == null)
                {
                    _orderCartImport.AddAccToCart(int.Parse(cartId), a.Id);
                }
                else
                {
                    _orderCartImport.MergeCartLogin(int.Parse(cartId), cartAcc.Id);
                }
                Response.Cookies.Delete("cart");
            }

            if (_account.CheckRole(a.Id, "admin"))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/admin/books");
            }

            return Redirect("index");
        }
    }
}
