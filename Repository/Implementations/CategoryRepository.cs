using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CommentReportRepository(IdealDiscussContext context) 
        { 
            _context = context;
        }
    }
}
