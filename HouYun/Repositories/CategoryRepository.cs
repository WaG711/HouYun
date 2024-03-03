using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IVideoRepository _videoRepository;

        public CategoryRepository(ApplicationDbContext context, IVideoRepository videoRepository)
        {
            _context = context;
            _videoRepository = videoRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                await DeleteVideos(category.CategoryId);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        private async Task DeleteVideos(Guid id)
        {
            var videos = await _context.Videos.Where(v => v.CategoryId == id).ToListAsync();
            foreach (var video in videos)
            {
                await _videoRepository.DeleteVideo(video.VideoId);
            }
        }
    }
}
