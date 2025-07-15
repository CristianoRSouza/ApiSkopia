using ApiTarefas.Data.Dtos;

namespace ApiTarefas.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectDto?> CreateProjectAsync(CreateProjectDto project);
        Task<bool> DeleteProjectAsync(int projectId);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}


