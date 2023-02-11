
using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        T Create(T entity);
        T Get(int id);
        T Update(T entity);
        void Remove(T entity);
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);
        bool Exists(Expression<Func<T, bool>> expression);
        int SaveChanges();
    }
}
