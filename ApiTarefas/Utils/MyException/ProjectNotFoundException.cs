using ApiTarefas.Utils.MyException;

public class ProjectNotFoundException : AppException
{
    public ProjectNotFoundException(int projectId)
        : base($"Projeto com ID {projectId} não foi encontrado.") { }
}