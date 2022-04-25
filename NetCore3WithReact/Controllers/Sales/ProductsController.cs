using Microsoft.AspNetCore.Mvc;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models.Sales;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers.Sales
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataManager _dataManager;

        public ProductsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {            
            return _dataManager.ProductRepository.GetAll();            
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            var product = _dataManager.ProductRepository.GetById(id);
            return product;            
        }

        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _dataManager.ProductRepository.Insert(value);
            _dataManager.Save();            
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Product value)
        {
            _dataManager.ProductRepository.Update(value);
            _dataManager.Save();            
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var productToDelete = _dataManager.ProductRepository.GetById(id);
            _dataManager.ProductRepository.Delete(productToDelete);
            _dataManager.Save();            
        }
    }
}
