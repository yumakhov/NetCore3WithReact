using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models.Sales;
using System.Collections.Generic;
using System.Linq;

namespace NetCore3WithReact.DAL.Repositories
{
    public class ProductRepository
    {
        //todo: change to factory
        private readonly IApplicationDbContext _dbContext;

        public ProductRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> Get()
        {
            //todo: add using with factory
            return _dbContext.Products.ToList();
        }
    }
}
