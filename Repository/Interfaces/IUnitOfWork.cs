using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Question> QuestionRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Flag> FlagRepository { get; }
        IRepository<QuestionReport> QuestionReportRepository { get; }
        IRepository<CommentReport> CommentReportRepository { get; }
        int SaveChanges();
    }
}
