using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces;

public interface IQuestionReportRepository : IRepository<QuestionReport>
{
    Task<QuestionReport> GetQuestionReport(string reportId);
    Task<List<QuestionReport>> GetQuestionReports(string questionId);
}
