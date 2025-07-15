namespace ApiTarefas.Data.Models
{
    public class History
    {
        public int Id { get; set; }
        public string FieldModified { get; set; } = string.Empty;
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
    }
}
