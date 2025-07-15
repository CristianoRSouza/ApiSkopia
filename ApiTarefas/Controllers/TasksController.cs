using ApiTarefas.Data.Dtos;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.MyException;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService ts) => _taskService = ts;

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetByProject(int projectId)
    {
        try
        {
            var tasks = await _taskService.GetTasksByProjectAsync(projectId);
            return Ok(tasks);
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPost("project/{projectId}")]
    public async Task<ActionResult<TaskDto>> Create(int projectId, CreateTaskDto dto)
    {
        try
        {
            var created = await _taskService.CreateTaskAsync(projectId, dto, "system");
            return CreatedAtAction(nameof(GetByProject), new { projectId }, created);
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (TaskLimitReachedException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
    {
        try
        {
            var result = await _taskService.UpdateTaskAsync(id, dto, "usuário");
            return Ok(result);
        }
        catch (AppException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor.", detail = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> AddComment(int id, CreateCommentDto dto)
    {
        try
        {
            await _taskService.AddCommentAsync(id, dto);
            return Ok();
        }
        catch (TaskNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("reports/performance")]
    public async Task<ActionResult<IEnumerable<TaskPerformanceReportDto>>> GetPerformance()
    {
        try
        {
            var report = await _taskService.GetPerformanceReportAsync();
            return Ok(report);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao gerar relatório de performance.", detail = ex.Message });
        }
    }
}
