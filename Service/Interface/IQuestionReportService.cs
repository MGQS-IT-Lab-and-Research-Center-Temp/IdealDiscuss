using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionReportDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionReportService
    {
        BaseResponseModel CreateQuestionReport(CreateQuestionReportDto request);
        BaseResponseModel DeleteQuestionReport(int id);
        BaseResponseModel UpdateQuestionReport(int id, UpdateQuestionReportDto request);
        QuestionReportResponseModel GetQuestionReport(int id);
        QuestionReportsResponseModel GetAllQuestionReport();
    }
}
