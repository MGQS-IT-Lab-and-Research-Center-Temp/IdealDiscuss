using IdealDiscuss.Models;
using IdealDiscuss.Models.Question;
using IdealDiscuss.Models.QuestionReport;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionReportService
    {
        BaseResponseModel CreateQuestionReport(CreateQuestionReportViewModel request);
        BaseResponseModel DeleteQuestionReport(string id);
        BaseResponseModel UpdateQuestionReport(string id, UpdateQuestionReportViewModel request);
        QuestionReportResponseModel GetQuestionReport(string reportId);
        QuestionReportsResponseModel GetQuestionReports(string questionId);
    }
}
