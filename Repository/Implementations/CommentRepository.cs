using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(IdealDiscussContext context) : base(context)
    {
    }

    public async Task<Comment> GetCommentWithReportList(string id)
    {
        var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.CommentReports)
                .ThenInclude(c => c.User)
                .Where(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();

        return comments;    
    }

    public async Task<Comment> GetCommentWithReportList(Expression<Func<Comment, bool>> expression)
    {
        var comments = await _context.Comments
            .Where(expression)
            .Include(u => u.User)
            .Include(c => c.CommentReports)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync();

        return comments;
    }

    public async Task<IList<CommentReport>> GetCommentReportsByCommentId(string commentId)
    {
        var commentReports = await _context.CommentReports
            .Include(cr => cr.User)
            .Include(c => c.Comment)
            .ThenInclude(c => c.User)
            .Where(c => c.CommentId.Equals(commentId))
            .ToListAsync();

        return commentReports;
    }

}
