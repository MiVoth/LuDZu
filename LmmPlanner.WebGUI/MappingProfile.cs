using AutoMapper;
using LmmPlanner.Entities.Models;

namespace LmmPlanner.WebGUI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<LmmPerson, LmmPersonExtented>();

        CreateMap<UnavailableInfo, Data.TheocData.Unavailable>();
        CreateMap<Data.TheocData.Unavailable, UnavailableInfo>();

        CreateMap<Data.TheocData.LmmSchedule, EditScheduleDto>();
        CreateMap<EditScheduleDto, Data.TheocData.LmmSchedule>();
        CreateMap<EditScheduleDto, Data.TheocData.LmmScheduleWrite>();
        // CreateMap<UserDto, User>();
    }
}
