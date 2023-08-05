using IdealDiscuss.DTOs.Flag;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Flag;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Service.Interface;

public interface IFlagService
{
    Task<BaseResponseModel> CreateFlag(FlagCreateDto createFlagDto);
    Task<BaseResponseModel> DeleteFlag(string flagId);
    Task<BaseResponseModel> UpdateFlag(string flagId, FlagUpdateDto FlagDto);
    Task<FlagResponseModel> GetFlag(string flagId);
    Task<FlagsResponseModel> GetAllFlag();
    Task<IEnumerable<SelectListItem>> SelectFlags();
}
