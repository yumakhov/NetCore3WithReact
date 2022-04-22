using NetCore3WithReact.DAL.EntityConfigurations;

namespace NetCore3WithReact.Data
{
    public class DataManagerFactory: IDataManagerFactory
    {
        public IApplicationDbContext _dbContext;
        public DataManagerFactory(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IDataManager Create()
        {
            return new DataManager(_dbContext);
        }
    }
}
