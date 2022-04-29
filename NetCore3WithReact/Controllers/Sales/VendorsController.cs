using Microsoft.AspNetCore.Mvc;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Entities.Sales;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers.Sales
{
    [Route("api/v1/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IDataManager _dataManager;

        public VendorsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IEnumerable<Vendor> Get()
        {           
            return _dataManager.VendorRepository.GetAll();            
        }

        [HttpGet("{id}")]
        public Vendor Get(Guid id)
        {
            _dataManager.VendorRepository.GetAll();
            var vendor = _dataManager.VendorRepository.GetById(id);
            return vendor;            
        }

        [HttpPost]
        public void Post([FromBody] Vendor value)
        {            
            _dataManager.VendorRepository.Insert(value);
            _dataManager.Save();            
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Vendor value)
        {
            _dataManager.VendorRepository.Update(value);
            _dataManager.Save();            
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var vendorToDelete = _dataManager.VendorRepository.GetById(id);
            if (vendorToDelete == null)
            {
                return;
            }

            _dataManager.VendorRepository.Delete(vendorToDelete);
            _dataManager.Save();            
        }
    }
}
