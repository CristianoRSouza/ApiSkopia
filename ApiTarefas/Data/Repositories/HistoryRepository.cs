using ApiTarefas.Data.Contexto;
using ApiTarefas.Data.Models;
using ApiTarefas.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class HistoryRepository : BaseRepository<History>, IHistoryRepository
{
    public HistoryRepository(AppDbContext context) : base(context) { }

    public async Task<List<History>> GetHistoryByTaskIdAsync(int taskId)
    {
        return await _dbSet.Where(h => h.TaskItemId == taskId).ToListAsync();
    }
}