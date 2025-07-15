using ApiTarefas.Utils.Enums;

namespace ApiTarefas.Data.Dtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }
        public Priority Priority { get; set; }
        public string User { get; set; } = string.Empty;
    }
}
