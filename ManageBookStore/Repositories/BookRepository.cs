using ManageBookStore.IRepositories;
using ManageBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Repositories
{
    public class BookRepository : IBook
    {
        Prn221ManageStoreContext _context;
        public BookRepository(Prn221ManageStoreContext context)
        {
            _context = context;
        }

        public bool CheckBookTitleExist(string title)
        {
            return _context.Books.Where(b=>b.Name.ToLower() == title.ToLower()).FirstOrDefault() != null;
        }

        public List<Book> GetAllInstockBook()
        {
            return _context.Books.Include(c=>c.Categories).Include(b=>b.ProductAcc).Where(b=>b.IsInStock==true).ToList();
        }

        public List<Book> GetListById(List<int> ids)
        {
            return _context.Books.Where(b=>ids.Contains(b.Id)).ToList();
        }

        void IBook.AddBook(Models.Book book)
        {
            _context.Books.Add(book);   
             _context.SaveChanges();
        }

        void IBook.DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        List<Models.Book> IBook.GetAllBooks()
        {
            return _context.Books.Include(c=>c.Categories).Include(x=>x.ProductAcc).ThenInclude(a=>a.IdNavigation).ToList();
        }

        List<Models.Book> IBook.GetBookByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        List<Models.Book> IBook.GetBookByCategory(int categoryId)
        {
            return _context.Books.Include(c=>c.Categories).Where(b=>b.Categories.FirstOrDefault(c=>c.Id==categoryId)!=null).ToList();
        }

        Models.Book IBook.GetBookById(int id)
        {
            return _context.Books.Include(c => c.Categories).Include(x => x.ProductAcc).ThenInclude(a => a.IdNavigation).FirstOrDefault(b => b.Id == id);
        }

        void IBook.UpdateBook(Models.Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
