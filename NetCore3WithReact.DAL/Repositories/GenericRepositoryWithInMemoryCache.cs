using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetCore3WithReact.DAL.Repositories
{
    public class GenericRepositoryWithInMemoryCache<T>: IGenericRepository<T> where T: class, IIdentityEntity
    {
        private readonly IGenericRepository<T> _decoratedRepository;
        private readonly InMemoryGenericStorage<T> _inMemoryStorage;
        private readonly string _cacheKeyPrefix;

        public GenericRepositoryWithInMemoryCache(IGenericRepository<T> decoratedRepository, string cacheKeyPrefix)
        {
            _decoratedRepository = decoratedRepository;
            _inMemoryStorage = new InMemoryGenericStorage<T>();
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

        public IEnumerable<T> GetAll(string includeProperties = "")
        {
            return _decoratedRepository.GetAll(includeProperties);
        }

        public T GetById(Guid id, string includeProperties = "")
        {
            var cachedItem = GetCachedItem(id);
            if (cachedItem != null)
            {
                return cachedItem;
            }

            var entity = _decoratedRepository.GetById(id, includeProperties);
            SetItemToCache(entity);
            return entity;
        }

        private T GetCachedItem(Guid id)
        {            
            var cacheKey = CreateItemCacheKey(id);
            return _inMemoryStorage.Get(cacheKey);           
        }

        private void SetItemToCache(T entity)
        {            
            var cacheKey = CreateItemCacheKey(entity.Id);
            _inMemoryStorage.Set(cacheKey, entity);
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
            var cacheKey = CreateItemCacheKey(id);
            _inMemoryStorage.Remove(cacheKey);
        }

        private string CreateItemCacheKey(Guid id)
        {
            return $"{_cacheKeyPrefix}_{id}";
        }
    }
}
