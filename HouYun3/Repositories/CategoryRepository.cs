using HouYun2.IRepositories;
using HouYun2.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HouYun3Context _context;

        public CategoryRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
