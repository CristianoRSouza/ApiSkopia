using ApiTarefas.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas.Data.Mapping
{
    public class TaskItemMapping : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Details)
                   .HasMaxLength(1000);

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .IsRequired();

            builder.Property(t => t.User)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.UpdatedAt)
                   .IsRequired();

            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
