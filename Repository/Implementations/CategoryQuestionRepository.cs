using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class CategoryQuestionRepository : BaseRepository<CategoryQuestion>, ICategoryQuestionRepository
    {
        public CategoryQuestionRepository(IdealDiscussContext context) 
        {
            _context = context;
        }
    }
}
