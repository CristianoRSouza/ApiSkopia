using ApiTarefas.Utils.MyException;

public class ProjectHasTasksException : AppException
{
    public ProjectHasTasksException(int projectId)
        : base($"Projeto com ID {projectId} não pode ser deletado porque possui tarefas vinculadas.") { }
}