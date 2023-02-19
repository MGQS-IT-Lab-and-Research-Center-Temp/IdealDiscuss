using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IdealDiscussContext context)
        {
            _context = context;
        }

        public User GetUser(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Include(x => x.Role).SingleOrDefault(expression);
        }
    }
}
