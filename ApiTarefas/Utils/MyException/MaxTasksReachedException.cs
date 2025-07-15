using ApiTarefas.Utils.MyException;

public class MaxTasksReachedException : AppException
{
    public MaxTasksReachedException()
        : base("Limite de 20 tarefas alcançado para este projeto.") { }
}