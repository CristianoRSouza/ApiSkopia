using ApiTarefas.Data.Contexto;
using ApiTarefas.Data.Models;
using ApiTarefas.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }

    public async Task<List<Comment>> GetCommentsByTaskIdAsync(int taskId)
    {
        return await _dbSet.Where(c => c.TaskItemId == taskId).ToListAsync();
    }
}
