using NetCore3WithReact.BusinessLogic.DataContracts;
using NetCore3WithReact.DAL.DataProviders;
using NetCore3WithReact.DAL.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly IDataManager _dataManager;

        public TagService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<TagData> GetProductTags(Guid productId)
        {
            var tags = _dataManager.TagRepository.Get(filter => filter.ProductTags.All(productTag => productTag.ProductId == productId));
            return tags.Select(ToTagData);
        }

        public TagData GetTagData(Guid id)
        {
            var tag = _dataManager.TagRepository.GetById(id);
            if (tag == null)
            {
                return null;
            }

            return ToTagData(tag);
        }

        public Tag GetTag(string name)
        {
            return _dataManager.TagRepository
                .Get(entity => entity.Name == name)
                .FirstOrDefault();
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
