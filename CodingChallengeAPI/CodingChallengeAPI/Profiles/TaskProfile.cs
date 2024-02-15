using AutoMapper;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;

namespace CodingChallengeAPI.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDTO>();
            CreateMap<TaskDTO, TaskEntity>();
            CreateMap<TaskEntity, TaskWithOutIDDTO>();
            CreateMap<TaskWithOutIDDTO, TaskEntity>();
        }
    }
}
