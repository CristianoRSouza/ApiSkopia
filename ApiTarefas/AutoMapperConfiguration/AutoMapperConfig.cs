using ApiTarefas.Data.Dtos;
using ApiTarefas.Data.Models;
using AutoMapper;

namespace ApiTarefas.AutoMapperConfiguration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();


            CreateMap<TaskItem, TaskDto>().ReverseMap();
            CreateMap<TaskItem, CreateTaskDto>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskDto>().ReverseMap();


            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();

            CreateMap<History, HistoryDto>().ReverseMap();


            CreateMap<TaskItem, TaskPerformanceReportDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.CompletedCount, opt => opt.Ignore());
        }
    }
}