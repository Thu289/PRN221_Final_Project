using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IBook
    {
        List<Book> GetAllBooks();

        Book GetBookById (int id);

        void AddBook (Book book);

        void UpdateBook(Book book);

        void DeleteBook (int id);

        List<Book> GetBookByCategory (int categoryId);

        List<Book> GetBookByAuthor(int authorId);

        bool CheckBookTitleExist (string title);

        List<Book> GetAllInstockBook();

        List<Book> GetListById(List<int> ids);
    }
}
