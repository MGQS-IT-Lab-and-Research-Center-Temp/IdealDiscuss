namespace IdealDiscuss.Models.CommentReport;

public class CommentReportResponseModel : BaseResponseModel
{
    public CommentReportViewModel Data { get; set; }
}

public class CommentReportsResponseModel : BaseResponseModel
{
    public List<CommentReportViewModel> Data { get; set; }
}
