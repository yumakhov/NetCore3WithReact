using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities;
using NetCore3WithReact.DAL.Entities.Sales;
using NetCore3WithReact.DAL.Entities.Tags;
using NetCore3WithReact.DAL.Repositories;
using NetCore3WithReact.DAL.Services;
using System;

namespace NetCore3WithReact.Data
{
    public class DataManager : IDataManager
    {    
        private bool _disposed;
        private readonly IApplicationDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;
        private readonly IFeatureSettingsService _featureSettingsService;

        public DataManager(IApplicationDbContext dbContext, IDistributedCache distributedCache, IFeatureSettingsService featureSettingsService)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
            _featureSettingsService = featureSettingsService;
        }

        private IGenericRepository<Product> productRepository;
        public IGenericRepository<Product> ProductRepository => productRepository ??= CreateGenericRepositoryWithCache<Product>(nameof(Product));

        private IGenericRepository<Vendor> vendorRepository;
        public IGenericRepository<Vendor> VendorRepository => vendorRepository ??= CreateGenericRepositoryWithCache<Vendor>(nameof(Vendor));

        private IGenericRepository<Tag> tagRepository;
        public IGenericRepository<Tag> TagRepository => tagRepository ??= CreateGenericRepositoryWithCache<Tag>(nameof(Tag));

        private IGenericRepository<T> CreateGenericRepositoryWithCache<T>(string cacheKeyPrefix) where T: class, IIdentityEntity
        {
            var baseRepository = new GenericRepository<T>(_dbContext);
            if (!_featureSettingsService.IsCacheEnabled)
            {
                return baseRepository;
            }

            return new GenericRepositoryWithDistributedCache<T>(baseRepository, _distributedCache, cacheKeyPrefix);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
