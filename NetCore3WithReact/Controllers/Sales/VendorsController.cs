using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetCore3WithReact.DAL.EntityConfigurations;
using NetCore3WithReact.DAL.Models.Sales;
using NetCore3WithReact.Utilities;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers.Sales
{
    [Route("api/v1/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private const string CacheKeyPrefix = "Vendors";

        private readonly IDataManagerFactory _dataManagerFactory;
        private readonly IDistributedCache _distributedCache;

        public VendorsController(IDataManagerFactory dataManagerFactory, IDistributedCache distributedCache)
        {
            _dataManagerFactory = dataManagerFactory;
            _distributedCache = distributedCache;
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
            var cacheKey = $"{CacheKeyPrefix}_{id}";
            var cachedItemJson = _distributedCache.GetString(cacheKey);
            if (cachedItemJson != null)
            {
                JsonSerializer.Deserialize<Vendor>(cachedItemJson);
            }

            using (var dataManager = _dataManagerFactory.Create())
            {
                var vendor = dataManager.VendorRepository.GetById(id);
                _distributedCache.SetString(cacheKey, JsonSerializer.Serialize(vendor));
                return vendor;
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
