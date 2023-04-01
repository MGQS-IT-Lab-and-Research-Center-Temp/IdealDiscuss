using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Repository.Implementations
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IdealDiscussContext context): base(context)
        {
        }

        public List<CommentReport> GetCommentReportsByCommentId(string commentId)
        {
            var comment = _context.CommentReports
                .Include(c => c.Comment)
                .ThenInclude(c => c.User)
                .Where(c => c.CommentId.Equals(commentId))
                .ToList();

            return comment;
        }

    }
}
