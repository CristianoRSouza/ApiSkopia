using ApiTarefas.Data.Dtos;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.MyException;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto dto)
    {
        try
        {
            var created = await _projectService.CreateProjectAsync(dto);
            return CreatedAtAction(nameof(GetProjects), new { id = created.Id }, created);
        }
        catch (AppException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (AppException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
