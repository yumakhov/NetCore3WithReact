using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.EntityConfigurations;

namespace NetCore3WithReact.Data
{
    public class DataManagerFactory: IDataManagerFactory
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;

        public DataManagerFactory(IApplicationDbContext dbContext, IDistributedCache distributedCache)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
        }
        
        public IDataManager Create()
        {
            return new DataManager(_dbContext, _distributedCache);
        }
    }
}
