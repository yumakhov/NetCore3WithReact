using NetCore3WithReact.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(Guid id, string includeProperties = "");
        void Insert(TEntity entityToInsert);
        void InsertRange(IEnumerable<TEntity> entitiesToInsert);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
    }
}
