using ApiTarefas.Utils.Enums;

namespace ApiTarefas.Data.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }
        public Priority Priority { get; set; }
        public MyTaskStatus Status { get; set; } = MyTaskStatus.Pending;
        public string User { get; set; } = "system";
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public List<History> ChangeHistory { get; set; } = new();

    }
}
