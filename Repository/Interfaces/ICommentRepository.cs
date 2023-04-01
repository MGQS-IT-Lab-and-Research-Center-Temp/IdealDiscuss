using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment GetCommentWithReportList(string id);
        Comment GetCommentWithReportList(Expression<Func<Comment, bool>> expression);
        List<CommentReport> GetCommentReportsByCommentId(string id);
    }
}
