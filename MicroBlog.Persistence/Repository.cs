using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroBlog.Entities;
using MicroBlog.Repository;

namespace MicroBlog.Persistence
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public T Create(T entity)
        {
            return Context.Set<T>().Add(entity);
        }

        public T Update(T entity)
        {
            if (entity == null)
                return null;

            var existing = Context.Set<T>().Find(entity.Id);
            if (null != existing)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
            }

            return existing;
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
