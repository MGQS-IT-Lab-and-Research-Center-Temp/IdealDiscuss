using IdealDiscuss.Models;
using IdealDiscuss.Models.Flag;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Service.Interface;

public interface IFlagService
{
    Task<BaseResponseModel> CreateFlag(CreateFlagViewModel createFlagDto);
    Task<BaseResponseModel> DeleteFlag(string flagId);
    Task<BaseResponseModel> UpdateFlag(string flagId, UpdateFlagViewModel FlagDto);
    Task<FlagResponseModel> GetFlag(string flagId);
    Task<FlagsResponseModel> GetAllFlag();
    Task<IEnumerable<SelectListItem>> SelectFlags();
}
