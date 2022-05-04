using NetCore3WithReact.BusinessLogic.DataContracts;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.BusinessLogic.Services
{
    public interface ITagService
    {
        TagData GetTagData(Guid id);
        IEnumerable<TagData> GetProductTags(Guid productId);
    }
}
