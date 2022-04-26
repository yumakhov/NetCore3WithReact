using NetCore3WithReact.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IIdentityModel
    {
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(Guid id);
        void Insert(TEntity entityToInsert);
        void InsertRange(IEnumerable<TEntity> entitiesToInsert);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
    }
}
