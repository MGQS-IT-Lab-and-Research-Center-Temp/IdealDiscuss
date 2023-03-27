using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Dtos;

namespace IdealDiscuss.Service.Interface
{
    public interface IFlagService
    {
        BaseResponseModel CreateFlag(CreateFlagDto createFlagDto);
        BaseResponseModel DeleteFlag(string flagId);
        BaseResponseModel UpdateFlag(string flagId, UpdateFlagDto FlagDto);
        FlagResponseModel GetFlag(string flagId);
        FlagsResponseModel GetAllFlag();
    }
}
