using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IApplicationRepository
    {
        Task<Application> GetApplicationById(Guid applicationId);
        Task AddApplication(Application application);
        Task DeleteApplication(Guid applicationId);
    }
}
