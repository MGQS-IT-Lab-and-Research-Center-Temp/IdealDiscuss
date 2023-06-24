using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(Expression<Func<User, bool>> expression);
    }
}
