using ApiTarefas.Utils.Enums;

namespace ApiTarefas.Data.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public string User { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
