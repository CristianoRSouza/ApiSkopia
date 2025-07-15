using ApiTarefas.Data.Dtos;
using ApiTarefas.Interfaces.Services;
using ApiTarefas.Utils.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests_Api.Controllers
{
    public class ControllerTasksTests
    {
        private readonly TasksController _controller;
        private readonly Mock<ITaskService> _mockService;

        public ControllerTasksTests()
        {
            _mockService = new Mock<ITaskService>();
            _controller = new TasksController(_mockService.Object);
        }

        [Fact]
        public async Task GetByProject_ReturnsOkResultWithTasks()
        {
            var projectId = 1;
            var fakeTasks = new List<TaskDto> { new TaskDto { Id = 1, Title = "Test" } };
            _mockService.Setup(s => s.GetTasksByProjectAsync(projectId)).ReturnsAsync(fakeTasks);

            var result = await _controller.GetByProject(projectId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var dto = new CreateTaskDto { Title = "Nova Tarefa", Priority = Priority.Medium };
            var taskResult = new TaskDto { Id = 1, Title = "Nova Tarefa" };
            _mockService.Setup(s => s.CreateTaskAsync(1, dto, "system")).ReturnsAsync(taskResult);

            var result = await _controller.Create(1, dto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Nova Tarefa", ((TaskDto)created.Value).Title);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteTaskAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            var noContentResult = Assert.IsAssignableFrom<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
    }
}