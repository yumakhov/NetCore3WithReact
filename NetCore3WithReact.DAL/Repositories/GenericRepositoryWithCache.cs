using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.Models;
using NetCore3WithReact.Utilities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public class GenericRepositoryWithCache<T>: IGenericRepository<T> where T: class, IIdentityModel
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
            InvalidateItemCache(entityToDelete.Id);
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
            var cachedItem = GetCachedItem<T>(id);
            if (cachedItem != null)
            {
                return cachedItem;
            }

            var entity = _decoratedRepository.GetById(id);
            SetItemToCache(entity);
            return entity;
        }

        private TEntity GetCachedItem<TEntity>(Guid id) where TEntity : class, IIdentityModel
        {
            try
            {
                var cacheKey = CreateItemCacheKey(id);
                var cachedItemJson = _distributedCache.GetString(cacheKey);
                if (cachedItemJson == null)
                {
                    return null;
                }

                return JsonSerializer.Deserialize<TEntity>(cachedItemJson);
            }
            catch (RedisConnectionException)
            {
                //todo: would be good to use circuit breaker pattern here
                return null;
            }
            catch (Exception)
            {
                //todo: log error
                return null;
            }
        }

        private void SetItemToCache<TEntity>(TEntity entity) where TEntity : class, IIdentityModel
        {
            try
            {
                var cacheKey = CreateItemCacheKey(entity.Id);
                _distributedCache.SetString(cacheKey, JsonSerializer.Serialize(entity));
            }
            catch (RedisConnectionException)
            {
                //todo: would be good to use circuit breaker pattern here
            }
            catch (Exception)
            {
                //todo: log error
            }
        }

        public void Insert(T entityToInsert)
        {
            _decoratedRepository.Insert(entityToInsert);
        }

        public void InsertRange(IEnumerable<T> entitiesToInsert)
        {
            _decoratedRepository.InsertRange(entitiesToInsert);
        }

        public void Update(T entityToUpdate)
        {
            _decoratedRepository.Update(entityToUpdate);
            InvalidateItemCache(entityToUpdate.Id);
        }

        private void InvalidateItemCache(Guid id)
        {            
            try
            {
                var cacheKey = CreateItemCacheKey(id);
                _distributedCache.Remove(cacheKey);
            }
            catch (RedisConnectionException)
            {
                //todo: would be good to use circuit breaker pattern here
            }
            catch (Exception)
            {
                //todo: log error
            }
        }

        private string CreateItemCacheKey(Guid id)
        {
            return $"{_cacheKeyPrefix}_{id}";
        }
    }
}
