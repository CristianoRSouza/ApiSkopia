using ApiTarefas.Data.Models;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> GetCommentsByTaskIdAsync(int taskId);
}
