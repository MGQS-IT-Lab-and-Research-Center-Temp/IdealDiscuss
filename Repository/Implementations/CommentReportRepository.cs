using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class CommentReportRepository : BaseRepository<CommentReport> , ICommentReportRepository
    {
        public CommentReportRepository(IdealDiscussContext context) 
        { 
            _context = context;
        }
    }
}
