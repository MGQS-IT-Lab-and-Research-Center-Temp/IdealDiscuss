namespace IdealDiscuss.Dtos.CommentReport
{
    public class CommentReportResponseModel: BaseResponseModel
    {
        public ViewCommentReportDto CommentReport { get; set; }
    }

    public class CommentReportsResponseModel : BaseResponseModel
    {
        public List<ViewCommentReportDto> CommentReports { get; set; }
    }
}
