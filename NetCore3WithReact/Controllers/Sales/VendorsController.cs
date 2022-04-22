using Microsoft.AspNetCore.Mvc;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models.Sales;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers.Sales
{
    [Route("api/v1/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IDataManagerFactory _dataManagerFactory;

        public VendorsController(IDataManagerFactory dataManagerFactory)
        {
            _dataManagerFactory = dataManagerFactory;
        }

        [HttpGet]
        public IEnumerable<Vendor> Get()
        {
            using(var dataManager = _dataManagerFactory.Create())
            {
                return dataManager.VendorRepository.GetAll();
            }
        }

        [HttpGet("{id}")]
        public Vendor Get(Guid id)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                return dataManager.VendorRepository.GetById(id);
            }
        }

        [HttpPost]
        public void Post([FromBody] Vendor value)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                dataManager.VendorRepository.Insert(value);
                dataManager.Save();
            }
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Vendor value)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                dataManager.VendorRepository.Update(value);
                dataManager.Save();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            using (var dataManager = _dataManagerFactory.Create())
            {
                var vendorToDelete = dataManager.VendorRepository.GetById(id);
                dataManager.VendorRepository.Delete(vendorToDelete);
                dataManager.Save();
            }
        }
    }
}
