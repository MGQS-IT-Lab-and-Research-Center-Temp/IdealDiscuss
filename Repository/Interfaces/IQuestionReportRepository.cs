using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionReportRepository : IRepository<QuestionReport>
    {
        List<QuestionReport> GetQuestionReports();
        QuestionReport GetQuestionReport(string id);
    }
}
