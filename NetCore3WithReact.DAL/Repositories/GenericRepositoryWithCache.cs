using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.Models;
using NetCore3WithReact.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public class GenericRepositoryWithCache<T>: IGenericRepository<T> where T: IIdentityModel
    {
        private readonly IGenericRepository<T> _decoratedRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly string _cacheKeyPrefix;

        public GenericRepositoryWithCache(IGenericRepository<T> decoratedRepository, IDistributedCache distributedCache, string cacheKeyPrefix)
        {
            _decoratedRepository = decoratedRepository;
            _distributedCache = distributedCache;
            _cacheKeyPrefix = cacheKeyPrefix;
        }

        public void Delete(T entityToDelete)
        {
            _decoratedRepository.Delete(entityToDelete);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return _decoratedRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<T> GetAll()
        {
            return _decoratedRepository.GetAll();
        }

        public T GetById(Guid id)
        {
            var cacheKey = CreateCacheKey(id);
            var cachedItemJson = _distributedCache.GetString(cacheKey);
            if (cachedItemJson != null)
            {
                JsonSerializer.Deserialize<T>(cachedItemJson);
            }

            return _decoratedRepository.GetById(id);
        }

        public void Insert(T entityToInsert)
        {
            _decoratedRepository.Insert(entityToInsert);
        }

        public void Update(T entityToUpdate)
        {
            _decoratedRepository.Update(entityToUpdate);
        }

        private string CreateCacheKey(Guid id)
        {
            return $"{_cacheKeyPrefix}_{id}";
        }
    }
}
