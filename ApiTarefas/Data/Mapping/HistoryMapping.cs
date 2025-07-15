using ApiTarefas.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class HistoryMapping : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.FieldModified)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(h => h.OldValue)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(h => h.NewValue)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(h => h.User)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(h => h.ModifiedAt)
               .IsRequired();

        builder.HasOne(h => h.TaskItem)
               .WithMany(t => t.ChangeHistory)
               .HasForeignKey(h => h.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}