using System.Collections.Generic;

namespace NetCore3WithReact.DAL.Entities.Tags
{
    public class Tag: IdentityEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }
}
