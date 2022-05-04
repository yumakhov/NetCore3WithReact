using NetCore3WithReact.BusinessLogic.DataContracts;
using NetCore3WithReact.DAL.Entities.Tags;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public interface ITagService
    {
        TagData GetTagData(Guid id);
        Tag GetTag(string name);
        IEnumerable<TagData> GetProductTags(Guid productId);
    }
}
