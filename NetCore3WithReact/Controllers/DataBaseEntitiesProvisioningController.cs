using Microsoft.AspNetCore.Mvc;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities.Sales;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.Controllers
{
    [Route("api/v1/db-entities")]
    [ApiController]
    public class DataBaseEntitiesProvisioningController : ControllerBase
    {
        private readonly IDataManager _dataManager;

        public DataBaseEntitiesProvisioningController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("")]
        [HttpPost]        
        public void Provision()
        {
            var vendors = new List<Vendor>();
            var products = new List<Product>();

            var vendorsCount = 100;
            var productsCount = 350;
            for(int vendorIndex = 0; vendorIndex < vendorsCount; vendorIndex++)
            {
                var vendor = CreateVendor($"Vendor_{vendorIndex + 1}");
                vendors.Add(vendor);
                for(int productIndex = 0; productIndex < productsCount; productIndex++)
                {
                    var product = CreateProduct($"Product_{vendorIndex + 1}_{productIndex + 1}", vendor);
                    products.Add(product);
                }
            }

            _dataManager.VendorRepository.InsertRange(vendors);
            _dataManager.ProductRepository.InsertRange(products);
            _dataManager.Save();
        }

        private static Vendor CreateVendor(string name)
        {
            return new Vendor
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        private static Product CreateProduct(string name, Vendor vendor)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Vendor = vendor
            };
        }
    }
}
