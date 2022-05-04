using NetCore3WithReact.BusinessLogic.DataContracts;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities.Sales;
using NetCore3WithReact.DAL.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataManager _dataManager;
        private readonly ITagService _tagService;

        public ProductService(IDataManager dataManager, ITagService tagService)
        {
            _dataManager = dataManager;
            _tagService = tagService;
        }

        public IEnumerable<ProductData> GetAllProducts()
        {
            var products = _dataManager.ProductRepository.GetAll("Vendor,Tags");
            return products.Select(ToProductData);
        }

        public ProductData Get(Guid id)
        {
            var product = _dataManager.ProductRepository.GetById(id, "Vendor,Tags");
            return ToProductData(product);
        }

        private ProductData ToProductData(Product product)
        {
            var tagDataItems = _tagService.GetProductTags(product.Id);

            return new ProductData
            {
                Id = product.Id,
                Name = product.Name,
                Vendor = ToVendorData(product.Vendor),
                Tags = tagDataItems.ToList()
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

        public TagData AddProductTag(Guid productId, string tagName)
        {
            var product = _dataManager.ProductRepository.GetById(productId);
            if (product == null)
            {
                throw new InvalidOperationException();
            }

            var tag = _tagService.GetTag(tagName);
            if (tag == null)
            {
                tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagName
                };

                _dataManager.TagRepository.Insert(tag);
            }            

            product.Tags.Add(new ProductTag
            {
                ProductId = product.Id,
                TagId = tag.Id
            });

            _dataManager.Save();

            return ToTagData(tag);
        }

        private static TagData ToTagData(Tag tag)
        {
            return new TagData
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}
