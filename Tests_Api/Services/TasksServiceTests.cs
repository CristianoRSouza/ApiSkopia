using ApiTarefas.Data.Dtos;
using ApiTarefas.Data.Models;
using ApiTarefas.Interfaces.Repositories;
using AutoMapper;
using Moq;

namespace Tests_Api.Services
{
    public class TaskServiceTests
    {
        private readonly TaskService _service;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;

        public TaskServiceTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new TaskService(_mockUow.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetTasksByProjectAsync_ReturnsMappedTasks()
        {
            var tasks = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test" } };
            var dtoList = new List<TaskDto> { new TaskDto { Id = 1, Title = "Test" } };
            _mockUow.Setup(x => x.TaskRepository.GetTasksByProjectIdAsync(1)).ReturnsAsync(tasks);
            _mockMapper.Setup(x => x.Map<List<TaskDto>>(tasks)).Returns(dtoList);

            var result = await _service.GetTasksByProjectAsync(1);

            Assert.Single(result);
            Assert.Equal("Test", result[0].Title);
        }
    }
}