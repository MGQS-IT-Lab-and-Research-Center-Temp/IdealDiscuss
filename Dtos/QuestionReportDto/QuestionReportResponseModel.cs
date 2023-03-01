namespace IdealDiscuss.Dtos.QuestionReportDto;

public class QuestionReportResponseModel : BaseResponseModel
{
    public ViewQuestionReportDto Report { get; set; }
}

public class QuestionReportsResponseModel : BaseResponseModel
{
    public List<ViewQuestionReportDto> Reports { get; set; }
}
