using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models.Sales;
using NetCore3WithReact.DAL.Repositories;
using System;

namespace NetCore3WithReact.Data
{
    public class DataManager : IDataManager
    {    
        private bool _disposed;
        private readonly IApplicationDbContext _dbContext;

        public DataManager(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Product> productRepository;
        public IGenericRepository<Product> ProductRepository => productRepository ??= new GenericRepository<Product>(_dbContext);

        private IGenericRepository<Vendor> vendorRepository;
        public IGenericRepository<Vendor> VendorRepository => vendorRepository ??= new GenericRepository<Vendor>(_dbContext);

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
