namespace IdealDiscuss.Dtos.FlagDto
{
    public class FlagResponseModel : BaseResponseModel
    {
        public ViewFlagDto Report { get; set; }
    }

    public class FlagsResponseModel : BaseResponseModel
    {
        public List<ViewFlagDto> Reports { get; set; }
    }
}
