using ApiTarefas.Data.Contexto;
using ApiTarefas.Utils.MyException;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas.Data.Repositories
{
    public class BaseRepository<Tentity> : IBaseRepository<Tentity> where Tentity : class
    {
        protected readonly AppDbContext _meuContexto;
        protected readonly DbSet<Tentity> _dbSet;

        public BaseRepository(AppDbContext myContext)
        {
            _meuContexto = myContext;
            _dbSet = _meuContexto.Set<Tentity>();
        }

        public Task Add(Tentity entidade)
        {
            if (entidade == null)
                throw new AppException("Entidade não pode ser nula.");

            _dbSet.Add(entidade);
            return Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new AppException($"Entidade com ID {id} não encontrada.");

            _dbSet.Remove(entity);
        }

        public async Task<Tentity> Get(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new AppException($"Entidade com ID {id} não encontrada.");

            return entity;
        }

        public async Task<IEnumerable<Tentity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public Task Update(Tentity entidade)
        {
            if (entidade == null)
                throw new AppException("Entidade não pode ser nula.");

            _dbSet.Update(entidade);
            return Task.CompletedTask;
        }
    }
}
