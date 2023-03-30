using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionReportDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionReportService
    {
        BaseResponseModel CreateQuestionReport(CreateQuestionReportDto request);
        BaseResponseModel DeleteQuestionReport(string id);
        BaseResponseModel UpdateQuestionReport(string id, UpdateQuestionReportDto request);
        QuestionReportResponseModel GetQuestionReport(string id);
        QuestionReportsResponseModel GetAllQuestionReport();
    }
}
