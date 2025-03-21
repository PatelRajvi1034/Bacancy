using EfCoreTask5.Models.DTOs;

namespace EfCoreTask5.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO?> GetProjectById(int id);
    }
}
