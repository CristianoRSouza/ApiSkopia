using ApiTarefas.Utils.MyException;

public class TransactionException : AppException
{
    public TransactionException(string operation)
        : base($"Erro ao executar operação de transação: {operation}") { }
}