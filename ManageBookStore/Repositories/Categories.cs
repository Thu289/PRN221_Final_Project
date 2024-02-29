using ManageBookStore.IRepositories;
using ManageBookStore.Models;

namespace ManageBookStore.Repositories
{
    public class Categories : ICategories
    {
        Prn221ManageStoreContext _context;
        public Categories(Prn221ManageStoreContext context)
        {
            _context = context;
        }

        public bool CheckNameCatExist(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower()) != null;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        void ICategories.AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        void ICategories.DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        List<Category> ICategories.GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        void ICategories.UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
