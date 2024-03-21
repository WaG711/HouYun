using HouYun.Data;
using HouYun.IRepositories;
using HouYun.Models;

namespace HouYun.Repositories
{
   public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> GetApplicationById(Guid applicationId)
        {
            return await _context.Applications.FindAsync(applicationId);
        }

        public async Task AddApplication(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplication(Guid applicationId)
        {
            var application = await _context.Applications.FindAsync(applicationId);

            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }
    }
}
