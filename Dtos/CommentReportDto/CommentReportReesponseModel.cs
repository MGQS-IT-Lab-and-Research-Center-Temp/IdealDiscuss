namespace IdealDiscuss.Dtos.CommentReport
{
    public class CommentReportResponseModel: BaseResponseModel
    {
        public ViewCommentReportDto Report { get; set; }
    }

    public class CommentReportsResponseModel : BaseResponseModel
    {
        public List<ViewCommentReportDto> Reports { get; set; }
    }
}
