namespace IdealDiscuss.Models.Flag;

public class FlagResponseModel : BaseResponseModel
{
    public FlagViewModel Data { get; set; }
}

public class FlagsResponseModel : BaseResponseModel
{
    public List<FlagViewModel> Data { get; set; }
}
