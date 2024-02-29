using ManageBookStore.Hubs;
using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ManageBookStore.Pages
{
    public class CartModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IAccount _account;
        IOrderCartImport _orderCartImport;
        IBook _book;
        IOrderDetails _orderDetails;
        IHubContext<ProductHub> _productHub;

        public Account User { get; set; }   

        List<OrderDetail> ItemInCart { get; set; } = new List<OrderDetail>();

        public OrderCartImport Cart { get; set; } = new OrderCartImport();

        private readonly HttpClient client = null;

        private readonly string urlApi = "";

        public CartModel(
            Prn221ManageStoreContext context, 
            IAccount account,
            IOrderCartImport orderCartImport, 
            IBook book, 
            IOrderDetails orderDetails, 
            IHubContext<ProductHub> hubContext
            )
        {
            _context = context;
            _account = account;
            _orderCartImport = orderCartImport;
            _book = book;
            _orderDetails = orderDetails;
            _productHub = hubContext;
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            urlApi = "http://localhost:5176/";
        }

        public async void GetAllBookInCart()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(urlApi + "getCart");
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

        public void OnGet()
        {
            string accId = HttpContext.Session.GetString("account");
            if (!string.IsNullOrEmpty(accId))
            {
                User = _account.GetAccountById(int.Parse(accId));
                Cart = _orderCartImport.GetCart(User.Id);
            }
            else
            {
                string cartId = Request.Cookies["cart"];
                if (!string.IsNullOrEmpty(cartId))
                {
                    Cart = _orderCartImport.GetById(int.Parse(cartId));
                }
            }
            if (Cart!=null && Cart.OrderDetails.Count > 0)
            {
                CalQtyCart();
            }
        }

        public void CalQtyCart()
        {
            List<Book> listBook = _book.GetListById(Cart.OrderDetails.Select(o=>o.ProductId).ToList());
            foreach(Book book in listBook)
            {
                OrderDetail detail = Cart.OrderDetails.FirstOrDefault(o => o.ProductId == book.Id);
                if (book.Quantity == 0)
                {
                    Cart.OrderDetails.Remove(detail);
                    _orderCartImport.UpdateOrderCartImport(Cart);
                }else if(book.Quantity < detail.Quantity)
                {
                    detail.Quantity = book.Quantity;
                    _orderDetails.UpdateOrderDetails(detail);
                }

            }
        }

        public IActionResult OnPostRemove(int pid, int cartId)
        {
            _orderCartImport.RemoveItem(cartId, pid);
            return Redirect("cart");
        }

        public IActionResult OnPostCheckout(int cartId)
        {
            string accId = HttpContext.Session.GetString("account");
            if (!string.IsNullOrEmpty(accId))
            {
                _orderCartImport.Checkout(cartId);
                Checkout(cartId);
                _productHub.Clients.All.SendAsync("Reload");
            }
            else
            {
                return Redirect("login");
            }
            return Redirect("cart");
        }

        public void Checkout(int cartId)
        {
            Cart = _orderCartImport.GetById(cartId);
            foreach(OrderDetail od in Cart.OrderDetails)
            {
                Book b = _book.GetBookById(od.ProductId);
                b.Quantity -= od.Quantity;
                _book.UpdateBook(b);
            }
        }

        public IActionResult OnPostUpdate(int cartId)
        {
            Cart = _orderCartImport.GetById(cartId);
            List<OrderDetail> removeOrderDetail = new List<OrderDetail>();
            foreach(OrderDetail od in Cart.OrderDetails)
            {
                int qty = 0;
                try
                {
                    qty = int.Parse(Request.Form[od.ProductId.ToString()]);
                }catch(Exception)
                {
                    qty = 0;
                }
                int qtyInStock =(int) _book.GetBookById(od.ProductId).Quantity;
                if (qty>qtyInStock)
                {
                    qty=qtyInStock;
                }
                if (qty <= 0)
                {
                    removeOrderDetail.Add(od);
                }
                else
                {
                    Cart.OrderDetails.FirstOrDefault(o => o.ProductId == od.ProductId).Quantity = qty;
                }
            }
            if (removeOrderDetail.Count > 0)
            {
                foreach(OrderDetail od in removeOrderDetail)
                {
                    Cart.OrderDetails.Remove(od);
                }
            }
            _orderCartImport.UpdateOrderCartImport(Cart);
            return Redirect("cart");
        }
    }
}
