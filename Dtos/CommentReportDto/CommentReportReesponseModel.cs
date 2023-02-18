namespace IdealDiscuss.Dtos.CommentReport
{
    public class CommentReportReesponseModel: BaseResponseModel
    {
        public ViewCommentReportDto Report { get; set; }
    }

    public class CommentReportsResponseModel : BaseResponseModel
    {
        public List<CommentReportReesponseModel> Reports { get; set; }
    }
}
