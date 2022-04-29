using NetCore3WithReact.BusinessLogic.DataContracts;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataManager _dataManager;

        public ProductService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<ProductData> GetAllProducts()
        {
            var products = _dataManager.ProductRepository.GetAll("Vendor");
            return products.Select(ToProductData);
        }

        public ProductData Get(Guid id)
        {
            var product = _dataManager.ProductRepository.GetById(id);
            return ToProductData(product);
        }

        private static ProductData ToProductData(Product product)
        {
            return new ProductData
            {
                Id = product.Id,
                Name = product.Name,
                Vendor = ToVendorData(product.Vendor)
            };
        }

        private static VendorData ToVendorData(Vendor vendor)
        {
            return new VendorData
            {
                Id = vendor.Id,
                Name = vendor.Name
            };
        }

        public void Post(ProductData value)
        {
            var productEntity = ToProductEntity(value);
            _dataManager.ProductRepository.Insert(productEntity);
            _dataManager.Save();
        }

        public void Put(ProductData value)
        {
            var productEntity = ToProductEntity(value);
            _dataManager.ProductRepository.Update(productEntity);
            _dataManager.Save();
        }

        private static Product ToProductEntity(ProductData productData)
        {
            return new Product
            {
                Id = productData.Id,
                Name = productData.Name,
                Vendor = ToVendorEntity(productData.Vendor)
            };
        }

        private static Vendor ToVendorEntity(VendorData vendorData)
        {
            return new Vendor
            {
                Id = vendorData.Id,
                Name = vendorData.Name
            };
        }

        public void Delete(Guid id)
        {
            var productToDelete = _dataManager.ProductRepository.GetById(id);
            _dataManager.ProductRepository.Delete(productToDelete);
            _dataManager.Save();
        }
    }
}
