using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageBookStore.Pages.Admin
{
    public class AddBookModel : PageModel
    {
        Prn221ManageStoreContext _context;
        IBook _bookRepository;
        ICategories _categories;
        IAccount _account;

        public List<Category> CategoriesList { get; set; } = new List<Category>();

        public Book BookUpdate { get; set; } = new Book();

        public List<Account> AccountList { get; set; } = new List<Account> ();

        public AddBookModel(Prn221ManageStoreContext context, IBook bookRepository, ICategories categories, IAccount account)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categories = categories;
            Cat = BookUpdate.Categories.Select(c => c.Id).ToList();
            _account = account; 
        }

        [BindProperty(Name = "id")]
        public int Id { get; set; }

        [BindProperty(Name = "title")]
        public string Name { get; set; }

        [BindProperty(Name = "code")]
        public string Code { get; set; }

        [BindProperty(Name = "description")]
        public string Description { get; set; }

        [BindProperty(Name = "qty")]
        public int? Qty { get; set; }

        [BindProperty(Name = "isInStock")]
        public bool? IsInStock { get; set; }

        [BindProperty(Name = "price")]
        public double? Price { get; set; }

        [BindProperty(Name = "img")]
        public string Image { get; set; }

        [BindProperty(Name = "cat")]
        public List<int> Cat { get; set; }


        [BindProperty(Name = "supplier")]
        public int Supplier { get; set; }

        public void OnGet(int? id)
        {
            CategoriesList = _categories.GetAllCategories();
            if (id != null)
            {
                BookUpdate = _bookRepository.GetBookById((int)id);
                SetUpdateBook();
            }
            AccountList = _account.GetAccountByRole("supplier");
        }

        public void SetUpdateBook()
        {
            Id = BookUpdate.Id;
            Name = BookUpdate.Name;
            Code = BookUpdate.Code;
            Description = BookUpdate.Description;
            Qty = BookUpdate.Quantity;
            IsInStock = BookUpdate.IsInStock;
            Price = BookUpdate.Price;
            Image = BookUpdate.Image;
            Cat = BookUpdate.Categories.Select(c => c.Id).ToList();
            if (BookUpdate.ProductAcc!=null && BookUpdate.ProductAcc.AccId != null)
            {
                int? supplierId = BookUpdate.ProductAcc.AccId;
                Supplier = (supplierId == null) ? 0 : (int)supplierId;
            }
        }

        public IActionResult OnPost() 
        {
            if (Id == null || Id == 0)
            {
                //Add
                if (_bookRepository.CheckBookTitleExist(Name) == true)
                {
                    ViewData["Error"] = "The book has this title is in system!";
                    return Page();
                }
                SetValue();
                _bookRepository.AddBook(BookUpdate);
                //ProductAcc productAcc = SaveProductAcc();
                //BookUpdate.ProductAcc = productAcc;
                //_bookRepository.UpdateBook(BookUpdate);
            }
            else
            {
                //Update
                BookUpdate = _bookRepository.GetBookById(Id);
                if (Name.ToLower() != BookUpdate.Name.ToLower() && _bookRepository.CheckBookTitleExist(Name) == true )
                {
                    ViewData["Error"] = "The book has this title is in system!";
                    return Page();
                }
                SetValue();
                //ProductAcc productAcc = SaveProductAcc();
                //BookUpdate.ProductAcc = productAcc;
                _bookRepository.UpdateBook(BookUpdate);
            }
            return Redirect("books");
        }

        public void SetValue()
        {
            BookUpdate.Name = Name;
            BookUpdate.Code = Code;
            BookUpdate.Description = Description;
            BookUpdate.Price = Price;
            BookUpdate.Quantity = Qty;
            BookUpdate.IsInStock = IsInStock;
            BookUpdate.Image = Image;
            BookUpdate.Categories.Clear();
            foreach (int catId in Cat)
            {
                BookUpdate.Categories.Add(_categories.GetCategoryById(catId));
            }
        }

        public ProductAcc SaveProductAcc()
        {
            ProductAcc productAcc = new ProductAcc();
            productAcc.ProductId = BookUpdate.Id;
            productAcc.AccId = Supplier;
            productAcc.Role = "supplier";
            _context.ProductAccs.Add(productAcc);
            _context.SaveChanges();
            return productAcc;
        }
    }
}
