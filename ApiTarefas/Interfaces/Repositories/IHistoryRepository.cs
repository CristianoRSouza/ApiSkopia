using ApiTarefas.Data.Models;

public interface IHistoryRepository : IBaseRepository<History>
{
    Task<List<History>> GetHistoryByTaskIdAsync(int taskId);
}