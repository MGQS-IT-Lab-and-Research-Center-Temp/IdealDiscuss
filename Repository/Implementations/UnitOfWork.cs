using IdealDiscuss.Context;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdealDiscussContext _context;
        public UnitOfWork(IdealDiscussContext context)
        {
            _context = context;
        }
        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
    }
}
