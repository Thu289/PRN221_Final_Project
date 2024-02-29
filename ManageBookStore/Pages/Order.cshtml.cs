using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ManageBookStore.Pages
{
    public class OrderModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IOrderCartImport _orderCartImport;
        IAccount _account;

        public Account User { get; set; } = new Account();

        public List<OrderCartImport> ListOrder {  get; set; }=new List<OrderCartImport>();

        private readonly HttpClient client = null;

        private readonly string urlApi = "";

        public OrderModel(Prn221ManageStoreContext context, IOrderCartImport orderCartImport, IAccount account)
        {
            _context = context;
            _orderCartImport = orderCartImport;
            _account = account;
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            urlApi = "http://localhost:5176/";
        }

        public async void GetAllOrder()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(urlApi + "getOrder");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                //Array a = JsonSerializer.Deserialize<Array>(content, option);
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(content, option);
                //return View(books);
            }
        }

        public IActionResult OnGet()
        {
            string accId = HttpContext.Session.GetString("account");
            if (!string.IsNullOrEmpty(accId))
            {
                User = _account.GetAccountById(int.Parse(accId));
                ListOrder = _orderCartImport.GetAllOrderOfAcc(User.Id);
                return Page();
            }
            else
            {
                return Redirect("login");
            }
        }
    }
}
