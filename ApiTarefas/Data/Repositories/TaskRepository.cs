using ApiTarefas.Data.Contexto;
using ApiTarefas.Data.Models;
using ApiTarefas.Data.Repositories;
using ApiTarefas.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context) { }

    public async Task<List<TaskItem>> GetTasksByProjectIdAsync(int projectId)
    {
        return await _dbSet
            .Include(t => t.Comments)
            .Include(t => t.ChangeHistory)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskWithDetailsAsync(int taskId)
    {
        return await _dbSet
            .Include(t => t.Comments)
            .Include(t => t.ChangeHistory)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }
}
