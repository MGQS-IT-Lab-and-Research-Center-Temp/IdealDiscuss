namespace IdealDiscuss.Models.CommentReport;

public class CommentReportResponseModel : BaseResponseModel
{
    public ViewCommentReportViewModel Data { get; set; }
}

public class CommentReportsResponseModel : BaseResponseModel
{
    public List<ViewCommentReportViewModel> Data { get; set; }
}
