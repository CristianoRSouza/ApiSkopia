using ApiTarefas.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiTarefas.Data.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Text)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(c => c.User)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.HasOne(c => c.TaskItem)
                   .WithMany(t => t.Comments)
                   .HasForeignKey(c => c.TaskItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
