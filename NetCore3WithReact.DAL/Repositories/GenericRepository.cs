using Microsoft.EntityFrameworkCore;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IIdentityModel
    {
        //todo: change to factory
        private readonly IApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

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
            
            return query.ToList();            
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public virtual void Insert(TEntity entityToInsert)
        {
            _dbSet.Add(entityToInsert);
        }

        public void InsertRange(IEnumerable<TEntity> entitiesToInsert)
        {
            _dbSet.AddRange(entitiesToInsert);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }
    }
}
