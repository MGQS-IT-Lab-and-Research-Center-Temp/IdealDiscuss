using AutoMapper;
using IdealDiscuss.DTOs.Flag;
using IdealDiscuss.Entities;

namespace IdealDiscuss.MapperConfig;

public class MapConfig : Profile
{
    public MapConfig()
    {
        //FlagDto mapping config
        CreateMap<FlagListDto, Flag>().ReverseMap();
        CreateMap<FlagDetailDto, Flag>().ReverseMap();
        CreateMap<FlagCreateDto, Flag>().ReverseMap();
        CreateMap<FlagUpdateDto, Flag>().ReverseMap();  

        //CategoryDto mapping config
    }
}
