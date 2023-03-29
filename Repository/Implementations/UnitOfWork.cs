using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Middlewares;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdealDiscussContext _context;
        private bool _disposed = false;

        public IRepository<Role> RoleRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Question> QuestionRepository { get; }
        public IRepository<Comment> CommentRepository { get; }
        public IRepository<Flag> FlagRepository { get; }
        public IRepository<QuestionReport> QuestionReportRepository { get; }
        public IRepository<CommentReport> CommentReportRepository { get; }

        public UnitOfWork(
            IdealDiscussContext context,
            IRepository<Role> roleRepository,
            IRepository<User> userRepository,
            IRepository<Category> categoryRepository,
            IRepository<Question> questionRepository,
            IRepository<Comment> commentRepository,
            IRepository<Flag> flagRepository,
            IRepository<QuestionReport> questionReportRepository,
            IRepository<CommentReport> commentReportRepository
            )
        {
            _context = context;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            CategoryRepository = categoryRepository;
            QuestionRepository = questionRepository;
            CommentRepository = commentRepository;
            FlagRepository = flagRepository;
            QuestionReportRepository = questionReportRepository;
            CommentReportRepository = commentReportRepository;
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
