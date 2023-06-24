namespace IdealDiscuss.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    IQuestionRepository Questions { get; }
    ICommentRepository Comments { get; }
    IFlagRepository Flags { get; }
    IQuestionReportRepository QuestionReports { get; }
    ICommentReportRepository CommentReports { get; }
    Task<int> SaveChangesAsync();
}