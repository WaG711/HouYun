using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategory(int categoryId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
