
using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        T Create(T entity);
        T Get(string id);
        T Update(T entity);
        void Remove(T entity);
        List<T> GetAllByIds(List<string> ids);
        T Get(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression = null);
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
