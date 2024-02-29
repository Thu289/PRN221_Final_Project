using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ManageBookStore.Pages
{
    public class OrderDetailModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IOrderCartImport _orderCartImport;
        IAccount _account;
        IOrderDetails _orderDetails;

        public Account User { get; set; } = new Account();

        public List<OrderDetail> ListOrderDetails { get; set; } = new List<OrderDetail>();

        public OrderCartImport Order { get; set; }=new OrderCartImport();

        private readonly HttpClient client = null;

        private readonly string urlApi = "";

        public OrderDetailModel(Prn221ManageStoreContext context, IOrderCartImport orderCartImport, IAccount account,IOrderDetails orderDetails)
        {
            _context = context;
            _orderCartImport = orderCartImport;
            _account = account;
            _orderDetails = orderDetails;
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            urlApi = "http://localhost:5176/";
        }
        public async void GetAllBookInOrder()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(urlApi + "getOD");
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
        public IActionResult OnGet(int? id)
        {
            if (id== null || id == 0)
            {
                return Redirect("login");
            }
            ListOrderDetails = _orderDetails.GetAllOrderDetails((int)id);
            Order = _orderCartImport.GetById((int)id);
            return Page();
        }

    }
}
