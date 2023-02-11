
using IdealDiscuss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
