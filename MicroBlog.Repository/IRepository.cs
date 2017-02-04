using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroBlog.Entities;

namespace MicroBlog.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
