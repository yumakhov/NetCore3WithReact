using NetCore3WithReact.BusinessLogic.DataContracts;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public interface IProductService
    {
        IEnumerable<ProductData> GetAllProducts();
        ProductData Get(Guid id);
        void Post(ProductData value);
        void Put(ProductData value);
        void Delete(Guid id);
        TagData AddProductTag(Guid productId, string tagName);
    }
}
