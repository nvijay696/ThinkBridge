using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThinkBridgeRepository.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

        TEntity Get(Func<TEntity, Boolean> where);

        int GetCount(Func<TEntity, Boolean> where);


        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
             string includeProperties);

        IEnumerable<TEntity> GetTop(int top, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        void Insert(TEntity obj);

        void InsertRange(List<TEntity> obj);

        void Update(TEntity obj);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(Func<TEntity, Boolean> where);
    }
}
