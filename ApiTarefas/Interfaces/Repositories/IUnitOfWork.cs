using ApiTarefas.Data.Repositories;

namespace ApiTarefas.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public ICommentRepository CommentRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IHistoryRepository HistoryRepository { get; }

        public 
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
