using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionReportRepository : IRepository<QuestionReport>
    {
        QuestionReport GetQuestionReport(string id);
    }
}
