namespace IdealDiscuss.Dtos.CommentReport
{
    public class CommentReportResponseModel: BaseResponseModel
    {
        public ViewCommentReportDto Data { get; set; }
    }

    public class CommentReportsResponseModel : BaseResponseModel
    {
        public List<ViewCommentReportDto> Data { get; set; }
    }
}
