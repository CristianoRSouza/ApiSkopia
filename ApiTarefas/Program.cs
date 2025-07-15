
using ApiTarefas.Data.Contexto;
using ApiTarefas.Data.Repositories;
using ApiTarefas.Interfaces.Repositories;
using ApiTarefas.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"), sqlOptions => sqlOptions.CommandTimeout(120)));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Serviços
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITaskService, TaskService>();



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
