using ApiTarefas.Utils.MyException;

public class TaskLimitReachedException : AppException
{
    public TaskLimitReachedException(int projectId)
        : base($"O projeto com ID {projectId} atingiu o limite máximo de 20 tarefas.") { }
}