using IdealDiscuss.Models;
using IdealDiscuss.Models.Flag;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdealDiscuss.Service.Interface
{
    public interface IFlagService
    {
        BaseResponseModel CreateFlag(CreateFlagViewModel createFlagDto);
        BaseResponseModel DeleteFlag(string flagId);
        BaseResponseModel UpdateFlag(string flagId, UpdateFlagViewModel FlagDto);
        FlagResponseModel GetFlag(string flagId);
        FlagsResponseModel GetAllFlag();
        IEnumerable<SelectListItem> SelectFlags();
    }
}
