using ApiTarefas.Data.Dtos;
using ApiTarefas.Data.Models;
using ApiTarefas.Interfaces.Repositories;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.Enums;
using ApiTarefas.Utils.MyException;
using AutoMapper;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork uow, IMapper mapper)
    {
        _unitOfWork = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _unitOfWork.ProjectRepository.GetAll();
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> CreateProjectAsync(CreateProjectDto dto)
    {
        var project = _mapper.Map<Project>(dto);
        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.ProjectRepository.Add(project);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _unitOfWork.ProjectRepository.GetProjectWithTasksAsync(id);
        if (project == null) throw new ProjectNotFoundException(id);

        if (project.Tasks.Any(t => t.Status != MyTaskStatus.Completed))
            throw new AppException("Não é possível remover o projeto com tarefas pendentes.");

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.ProjectRepository.Delete(id);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
