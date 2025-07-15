using System.ComponentModel.DataAnnotations;

namespace ApiTarefas.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
