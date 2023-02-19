using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Dtos;

namespace IdealDiscuss.Service.Interface
{
    public interface IFlagService
    {
        BaseResponseModel CreateFlag(CreateFlagDto createFlagDto);
        BaseResponseModel DeleteFlag(int flagId);
        BaseResponseModel UpdateFlag(int flagId, UpdateFlagDto FlagDto);
        FlagResponseModel GetFlag(int flagId);
        FlagsResponseModel GetAllFlag();
    }
}
