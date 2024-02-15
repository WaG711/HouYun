using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> AddCategory(Category category);
        Task DeleteCategory(Guid id);
    }
}
