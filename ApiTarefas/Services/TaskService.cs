using ApiTarefas.Data.Dtos;
using ApiTarefas.Data.Models;
using ApiTarefas.Interfaces.Repositories;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.Enums;
using ApiTarefas.Utils.MyException;
using AutoMapper;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork uow, IMapper mapper)
    {
        _unitOfWork = uow;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetTasksByProjectAsync(int projectId)
    {
        var tasks = await _unitOfWork.TaskRepository.GetTasksByProjectIdAsync(projectId);
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> CreateTaskAsync(int projectId, CreateTaskDto dto, string user)
    {
        var project = await _unitOfWork.ProjectRepository.GetProjectWithTasksAsync(projectId);
        if (project == null) throw new ProjectNotFoundException(projectId);
        if (project.Tasks.Count >= 20) throw new TaskLimitReachedException(projectId);

        var task = _mapper.Map<TaskItem>(dto);
        task.ProjectId = projectId;
        task.Priority = dto.Priority;

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.TaskRepository.Add(task);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<TaskDto>(task);
    }

    public async Task<UpdateTaskDto> UpdateTaskAsync(int taskId, UpdateTaskDto dto, string user)
    {
        var existing = await _unitOfWork.TaskRepository.GetTaskWithDetailsAsync(taskId);
        if (existing == null) throw new TaskNotFoundException(taskId);

        if (dto.Priority != existing.Priority)
            throw new AppException("A prioridade não pode ser alterada após a criação da tarefa.");

        await _unitOfWork.BeginTransactionAsync();

        if (existing.Status != dto.Status)
        {
            await _unitOfWork.HistoryRepository.Add(new History
            {
                TaskItemId = taskId,
                FieldModified = "Status",
                OldValue = existing.Status.ToString(),
                NewValue = dto.Status.ToString(),
                User = user,
                ModifiedAt = DateTime.UtcNow
            });
            existing.Status = dto.Status;
        }

        if (!string.IsNullOrEmpty(dto.Details) && existing.Details != dto.Details)
        {
            await _unitOfWork.HistoryRepository.Add(new History
            {
                TaskItemId = taskId,
                FieldModified = "Details",
                OldValue = existing.Details ?? "",
                NewValue = dto.Details,
                User = user,
                ModifiedAt = DateTime.UtcNow
            });
            existing.Details = dto.Details;
        }

        await _unitOfWork.TaskRepository.Update(existing);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<UpdateTaskDto>(existing);
    }


    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var task = await _unitOfWork.TaskRepository.Get(taskId);
        if (task == null) throw new TaskNotFoundException(taskId);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.TaskRepository.Delete(task.Id);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> AddCommentAsync(int taskId, CreateCommentDto dto)
    {
        var task = await _unitOfWork.TaskRepository.Get(taskId);
        if (task == null) throw new TaskNotFoundException(taskId);

        await _unitOfWork.BeginTransactionAsync();

        var comment = _mapper.Map<Comment>(dto);
        comment.TaskItemId = taskId;
        await _unitOfWork.CommentRepository.Add(comment);

        await _unitOfWork.HistoryRepository.Add(new History
        {
            TaskItemId = taskId,
            FieldModified = "Comment",
            OldValue = "",
            NewValue = dto.Text,
            User = dto.User,
            ModifiedAt = DateTime.UtcNow
        });

        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<List<TaskPerformanceReportDto>> GetPerformanceReportAsync()
    {
        var allTasks = await _unitOfWork.TaskRepository.GetAll();
        var cutoff = DateTime.UtcNow.AddDays(-30);

        var report = allTasks
            .Where(t => t.Status == MyTaskStatus.Completed
                     && t.UpdatedAt >= cutoff)
            .GroupBy(t => t.User)
            .Select(g => new TaskPerformanceReportDto
            {
                User = g.Key,
                CompletedCount = g.Count()
            }).ToList();

        return report;
    }
}
