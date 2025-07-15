using ApiTarefas.Data.Contexto;
using ApiTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }
        public async Task<Project?> GetProjectWithTasksAsync(int projectId)
        {
            var project = await _dbSet.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
                throw new ProjectNotFoundException(projectId);

            return project;
        }
    }
}
