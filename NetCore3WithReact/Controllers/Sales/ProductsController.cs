using Microsoft.AspNetCore.Mvc;
using NetCore3WithReact.BusinessLogic.DataContracts;
using NetCore3WithReact.BusinessLogic.Services;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers.Sales
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductData> Get()
        {            
            return _productService.GetAllProducts();            
        }

        [HttpGet("{id}")]
        public ProductData Get(Guid id)
        {
            return _productService.Get(id);            
        }

        [HttpPost]
        public void Post([FromBody] ProductData value)
        {
            _productService.Post(value);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] ProductData value)
        {
            _productService.Put(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _productService.Delete(id);
        }
    }
}
