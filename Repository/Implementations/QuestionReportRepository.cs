using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class QuestionReportRepository : BaseRepository<QuestionReport> , IQuestionReportRepository
    {
        public QuestionReportRepository(IdealDiscussContext context)
        {
            _context = context;
        }
    }
}
