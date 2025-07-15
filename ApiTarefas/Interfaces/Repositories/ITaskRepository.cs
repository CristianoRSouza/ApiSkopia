using ApiTarefas.Data.Models;
using ApiTarefas.Data.Repositories;

namespace ApiTarefas.Interfaces.Repositories
{
    public interface ITaskRepository : IBaseRepository<TaskItem>
    {
        Task<List<TaskItem>> GetTasksByProjectIdAsync(int projectId);
        Task<TaskItem?> GetTaskWithDetailsAsync(int taskId);
    }
}
