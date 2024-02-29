using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface ICategories
    {
        List<Category> GetAllCategories();

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(int categoryId);

        bool CheckNameCatExist(string name);

        Category GetCategoryById(int categoryId);
    }
}
