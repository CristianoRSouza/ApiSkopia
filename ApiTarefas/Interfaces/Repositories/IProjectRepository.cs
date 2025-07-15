using ApiTarefas.Data.Models;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<Project?> GetProjectWithTasksAsync(int projectId);
}
