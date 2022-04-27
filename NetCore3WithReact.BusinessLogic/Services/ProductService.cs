using NetCore3WithReact.DAL.EntityConfigurations;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataManager _dataManager;

        public ProductService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
    }
}
