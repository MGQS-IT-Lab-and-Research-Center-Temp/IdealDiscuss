namespace IdealDiscuss.Dtos.FlagDto
{
    public class FlagResponseModel : BaseResponseModel
    {
        public ViewFlagDto Data { get; set; }
    }

    public class FlagsResponseModel : BaseResponseModel
    {
        public List<ViewFlagDto> Data { get; set; }
    }
}
