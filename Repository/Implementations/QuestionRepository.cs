using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IdealDiscussContext context)
        {
            _context = context;
        }
    }
}
