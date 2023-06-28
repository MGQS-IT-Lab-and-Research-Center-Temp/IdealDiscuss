using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface ICommentReportRepository : IRepository<CommentReport>
    {
        Task<List<CommentReport>> GetCommentReports();
        Task<CommentReport> GetCommentReport(string id);
    }
}
