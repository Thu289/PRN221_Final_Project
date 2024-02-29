using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ManageBookStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IOrderCartImport _orderCartImport;
        IOrderDetails _orderDetails;
        IAccount _account;

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Book> Books { get; set; } = new List<Book>();

        public string SearchQuery { get; set; }

        public Paging<Book> PagingPage { get; set; }=new Paging<Book>() ;

        public List<int> ListItemPerPage { get; set; } = new List<int> { 5, 10, 15, 20 };

        [BindProperty(Name ="item")]
        public int ItemPerPage { get; set; } = 5;

        public Account User { get;set; }

        private readonly HttpClient client = null;

        private readonly string urlApi = "";

        public IndexModel(ILogger<IndexModel> logger, IBook book, ICategories categories, IOrderCartImport orderCartImport, IOrderDetails orderDetails, IAccount account)
        {
            _logger = logger;
            _bookRepository = book;
            _categories = categories;
            _orderCartImport = orderCartImport;
            _orderDetails = orderDetails;   
            _account = account;
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            urlApi = "http://localhost:5176/";
        }

        public async void GetAllBook()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(urlApi + "getBook");
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

        public void OnGet(string? search, int? cat, int? page, int? item)
        {

            string accId = HttpContext.Session.GetString("account");
            if (!string.IsNullOrEmpty(accId))
            {
                User = _account.GetAccountById(int.Parse(accId));
            }
            Categories = _categories.GetAllCategories();
            Books = _bookRepository.GetAllInstockBook();
            if (cat != null && cat!= 0)
            {
                Books = _bookRepository.GetBookByCategory((int)cat);
            }
            if (!string.IsNullOrEmpty(search))
            {
                SearchQuery = search.ToLower();
                if (Books != null)
                {
                    Books = Books.Where(b => b.Name.ToLower().Contains(SearchQuery) || (b.Description!= null && b.Description.Contains(SearchQuery))).ToList();
                }
            }
            if (page == null)
            {
                try
                {
                    page = int.Parse(Request.Query["page"]);
                }catch(Exception) 
                {
                    page = 1;
                }
            }
            if (item ==null || item  <= 0)
            {
                item = 5;
            }
            ItemPerPage = (int)item;
            PagingPage.ListItem = Books;
            PagingPage.ItemPerPage = ItemPerPage;
            PagingPage.CalTotalPage();
            if (page<1 || page > PagingPage.TotalPage)
            {
                page = 1;
            }
            Books = PagingPage.GetListPageIndex((int)page);
        }

        public IActionResult OnPost(int pid)
        {
            string accId = HttpContext.Session.GetString("account");
            OrderDetail cartItem = new OrderDetail();
            cartItem.ProductId = pid;
            cartItem.Quantity = 1;
            cartItem.Price = _bookRepository.GetBookById(pid).Price;
            if (accId == null)
            {
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(3)
                };

                string cartId = Request.Cookies["cart"];
                if (string.IsNullOrEmpty(cartId) == true || cartId == "[]")
                {
                    OrderCartImport cart = new OrderCartImport();
                    cart.TotalPrice = 0;
                    cart.FinalPrice = 0;
                    cart.Type = "cart";
                    cart.RemoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                    _orderCartImport.AddOrderCartImport(cart);
                    _orderCartImport.AddItemToCart(cart.Id, cartItem);
                    Response.Cookies.Append(
                    "cart",
                    cart.Id.ToString(),
                    cookieOptions
                    );
                }
                else
                {
                    _orderCartImport.AddItemToCart(int.Parse(cartId), cartItem);
                }
            }
            else
            {
                OrderCartImport cartAcc = _orderCartImport.GetCart(int.Parse(accId));
                if (cartAcc != null)
                {
                    _orderCartImport.AddItemToCart(cartAcc.Id, cartItem);
                }
                else
                {
                    OrderCartImport cart = new OrderCartImport();
                    cart.TotalPrice = 0;
                    cart.FinalPrice = 0;
                    cart.Type = "cart";
                    cart.AccId = int.Parse(accId);
                    cart.RemoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                    _orderCartImport.AddOrderCartImport(cart);
                    _orderCartImport.AddItemToCart(cart.Id, cartItem);
                }
            }
            return Redirect("index");
        }
    }
}