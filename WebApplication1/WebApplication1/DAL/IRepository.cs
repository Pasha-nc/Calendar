using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "",
                                    int? page = null, int? amount = null);
        TEntity Get(object id);

        void Insert(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Delete(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "",
                                                int? page = null, int? amount = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) query = orderBy(query);

            if (page.HasValue && amount.HasValue) query = query.Skip((page.Value - 1) * amount.Value).Take(amount.Value);

            return query;
        }
        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            TEntity entityToDelete = dbSet.Find(id);

            if (entityToDelete == null) throw new InvalidOperationException("There is no records with such id");
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null) dbSet.RemoveRange(dbSet.Where(predicate));
            else dbSet.RemoveRange(dbSet);
        }

        public virtual void Update(TEntity entity)
        {
            //dbSet.Attach(entity);
            //var entry = dbContext.Entry(entity);
            var entry = dbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }
    }
}
