using ApiTarefas.Data.Dtos;
using ApiTarefas.Data.Models;
using ApiTarefas.Interfaces.Repositories;
using AutoMapper;
using Moq;

namespace Tests_Api.Services
{
    public class ProjectServiceTests
    {
        private readonly ProjectService _service;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;

        public ProjectServiceTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProjectService(_mockUow.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllProjectsAsync_ReturnsMappedProjects()
        {
            var projects = new List<Project> { new Project { Id = 1, Name = "Projeto 1" } };
            var dtos = new List<ProjectDto> { new ProjectDto { Id = 1, Name = "Projeto 1" } };
            _mockUow.Setup(u => u.ProjectRepository.GetAll()).ReturnsAsync(projects);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProjectDto>>(projects)).Returns(dtos);

            var result = await _service.GetAllProjectsAsync();

            Assert.Single(result);
        }
    }
}
