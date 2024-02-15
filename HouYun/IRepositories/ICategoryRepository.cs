using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> AddCategory(Category category);
        Task DeleteCategory(Guid id);
    }
}
