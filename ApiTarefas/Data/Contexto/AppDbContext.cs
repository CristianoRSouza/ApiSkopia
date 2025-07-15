using ApiTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas.Data.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Project> Projetos { get; set; }
        public DbSet<TaskItem> Tarefas { get; set; }
        public DbSet<History> Historicos { get; set; }
        public DbSet<Comment> Comentarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
