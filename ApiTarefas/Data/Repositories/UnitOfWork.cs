using ApiTarefas.Data.Contexto;
using ApiTarefas.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiTarefas.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _myContext { get; set; }

        private IDbContextTransaction? _transaction;
        public ICommentRepository CommentRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IHistoryRepository HistoryRepository { get; }

        public UnitOfWork(AppDbContext myContext, ICommentRepository commentRepository, ITaskRepository taskRepository,
            IProjectRepository projectRepository, IHistoryRepository historyRepository)
        {
            CommentRepository = commentRepository;
            TaskRepository = taskRepository;
            ProjectRepository = projectRepository;
            HistoryRepository = historyRepository;
            _myContext = myContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _myContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            await _myContext.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
