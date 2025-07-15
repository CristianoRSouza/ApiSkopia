using ApiTarefas.Utils.MyException;

public class TaskNotFoundException : AppException
{
    public TaskNotFoundException(int taskId)
        : base($"Tarefa com ID {taskId} não foi encontrada.") { }
}