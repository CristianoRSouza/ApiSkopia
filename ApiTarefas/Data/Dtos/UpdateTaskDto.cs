using ApiTarefas.Utils.Enums;

namespace ApiTarefas.Data.Dtos
{
    public class UpdateTaskDto
    {
        public string? Details { get; set; }
        public MyTaskStatus Status { get; set; }
        public Priority Priority { get; set; } // <== Adicione isso
    }

}
