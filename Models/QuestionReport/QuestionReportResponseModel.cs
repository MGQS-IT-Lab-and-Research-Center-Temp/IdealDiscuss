namespace IdealDiscuss.Models.QuestionReport;

public class QuestionReportResponseModel : BaseResponseModel
{
    public QuestionReportViewModel Data { get; set; }
}

public class QuestionReportsResponseModel : BaseResponseModel
{
    public List<QuestionReportViewModel> Data { get; set; }
}
