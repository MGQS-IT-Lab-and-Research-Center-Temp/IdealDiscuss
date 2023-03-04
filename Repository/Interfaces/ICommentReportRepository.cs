using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface ICommentReportRepository : IRepository<CommentReport>
    {
        List<CommentReport> GetCommentReports();
        CommentReport GetComment(int id);
    }
}
