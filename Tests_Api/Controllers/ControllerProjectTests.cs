using ApiTarefas.Data.Dtos;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.MyException;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests_Api.Controllers
{
    public class ProjectsControllerTests
    {
        private readonly Mock<IProjectService> _projectServiceMock = new();
        private readonly ProjectsController _controller;

        public ProjectsControllerTests()
        {
            _controller = new ProjectsController(_projectServiceMock.Object);
        }

        [Fact]
        public async Task GetProjects_ReturnsOkResult_WithListOfProjects()
        {
            var mockProjects = new List<ProjectDto> { new() { Id = 1, Name = "Test" } };
            _projectServiceMock.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(mockProjects);

            var result = await _controller.GetProjects();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(mockProjects, okResult.Value);
        }

        [Fact]
        public async Task DeleteProject_Success_ReturnsNoContent()
        {
            _projectServiceMock.Setup(s => s.DeleteProjectAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteProject(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProject_NotFound_ReturnsNotFound()
        {
            _projectServiceMock
                .Setup(s => s.DeleteProjectAsync(1))
                .ThrowsAsync(new ProjectNotFoundException(1));

            var result = await _controller.DeleteProject(1);

            var notFoundResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.NotNull(notFoundResult.Value);
        }
    }
}