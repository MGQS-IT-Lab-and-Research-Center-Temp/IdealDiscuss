using IdealDiscuss.Context;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdealDiscussContext _context;
        private bool _disposed = false;
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public ICategoryRepository Categories { get; }
        public IQuestionRepository Questions { get; }
        public ICommentRepository Comments { get; }
        public IFlagRepository Flags { get; }
        public IQuestionReportRepository QuestionReports { get; }
        public ICommentReportRepository CommentReports { get; }

        public UnitOfWork(
            IdealDiscussContext context,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IQuestionRepository questionRepository,
            ICommentRepository commentRepository,
            IFlagRepository flagRepository,
            IQuestionReportRepository questionReportRepository,
            ICommentReportRepository commentReportRepository)
        {
            _context = context;
            Roles = roleRepository;
            Users = userRepository;
            Categories = categoryRepository;
            Questions = questionRepository;
            Comments = commentRepository;
            Flags = flagRepository;
            QuestionReports = questionReportRepository;
            CommentReports = commentReportRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
