using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models;
using NetCore3WithReact.DAL.Models.Sales;
using NetCore3WithReact.DAL.Repositories;
using System;

namespace NetCore3WithReact.Data
{
    public class DataManager : IDataManager
    {    
        private bool _disposed;
        private readonly IApplicationDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;

        public DataManager(IApplicationDbContext dbContext, IDistributedCache distributedCache)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
        }

        private IGenericRepository<Product> productRepository;
        public IGenericRepository<Product> ProductRepository => productRepository ??= CreateGenericRepositoryWithCache<Product>(nameof(Product));

        private IGenericRepository<Vendor> vendorRepository;
        public IGenericRepository<Vendor> VendorRepository => vendorRepository ??= CreateGenericRepositoryWithCache<Vendor>(nameof(Vendor));

        private IGenericRepository<T> CreateGenericRepositoryWithCache<T>(string cacheKeyPrefix) where T: class, IIdentityModel
        {
            var baseRepository = new GenericRepository<T>(_dbContext);
            return new GenericRepositoryWithCache<T>(baseRepository, _distributedCache, cacheKeyPrefix);
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
