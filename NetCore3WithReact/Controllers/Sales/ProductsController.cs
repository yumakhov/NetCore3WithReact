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
        private readonly IDataManagerFactory _dataManagerFactory;

        public ProductsController(IDataManagerFactory dataManagerFactory)
        {
            _dataManagerFactory = dataManagerFactory;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            using(var dataManager = _dataManagerFactory.Create())
            {
                return dataManager.ProductRepository.GetAll();
            }
        }

        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                var product = dataManager.ProductRepository.GetById(id);
                return product;
            }
        }

        [HttpPost]
        public void Post([FromBody] Product value)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                dataManager.ProductRepository.Insert(value);
                dataManager.Save();
            }
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Product value)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                dataManager.ProductRepository.Update(value);
                dataManager.Save();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                var productToDelete = dataManager.ProductRepository.GetById(id);
                dataManager.ProductRepository.Delete(productToDelete);
                dataManager.Save();
            }
        }
    }
}
