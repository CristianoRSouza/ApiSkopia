using ApiTarefas.Data.Dtos;

namespace ApiTarefas.Interfaces.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksByProjectAsync(int projectId);
        Task<TaskDto?> CreateTaskAsync(int projectId, CreateTaskDto task, string user);
        Task<UpdateTaskDto?> UpdateTaskAsync(int taskId, UpdateTaskDto updatedTask, string user);
        Task<bool> DeleteTaskAsync(int taskId);
        Task<bool> AddCommentAsync(int taskId, CreateCommentDto comment);
        Task<List<TaskPerformanceReportDto>> GetPerformanceReportAsync();

    }
}
