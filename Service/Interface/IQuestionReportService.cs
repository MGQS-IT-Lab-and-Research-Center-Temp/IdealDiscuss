using IdealDiscuss.Models;
using IdealDiscuss.Models.QuestionReport;

namespace IdealDiscuss.Service.Interface;

public interface IQuestionReportService
{
    Task<BaseResponseModel> CreateQuestionReport(CreateQuestionReportViewModel request);
    Task<BaseResponseModel> DeleteQuestionReport(string id);
    Task<BaseResponseModel> UpdateQuestionReport(string id, UpdateQuestionReportViewModel request);
    Task<QuestionReportResponseModel> GetQuestionReport(string reportId);
    Task<QuestionReportsResponseModel> GetQuestionReports(string questionId);
}
