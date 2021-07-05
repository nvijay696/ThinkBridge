using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThinkBridgeDataLayer;

namespace ThinkBridgeRepository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        internal readonly ThinkBridgeDBContext dbContext;
        internal readonly DbSet<TEntity> dbSet;

        public GenericRepository(ThinkBridgeDBContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = GetById(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> entities = dbSet.Where<TEntity>(where).AsQueryable();
            if (entities.Any())
            {
                dbSet.RemoveRange(entities);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }


        public int GetCount(Func<TEntity, Boolean> where)
        {
            return dbSet.Where(where).Count();

        }

        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual TEntity Get(Func<TEntity, Boolean> where)
        {
            try
            {
                var result = dbSet.Where(where).FirstOrDefault<TEntity>();
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "")
        {

            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetTop(int top, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {

            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).Take(top).ToList();
            }
            else
            {
                return query.Take(top).ToList();
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                dbSet.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void InsertRange(List<TEntity> entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                dbSet.AddRange(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
