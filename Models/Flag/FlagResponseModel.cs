using IdealDiscuss.DTOs.Flag;

namespace IdealDiscuss.Models.Flag;

public class FlagResponseModel : BaseResponseModel
{
    public FlagDetailDto Data { get; set; }
}

public class FlagsResponseModel : BaseResponseModel
{
    public List<FlagListDto> Data { get; set; }
}
