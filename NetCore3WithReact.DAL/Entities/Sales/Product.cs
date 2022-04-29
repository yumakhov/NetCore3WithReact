using NetCore3WithReact.DAL.Entities.Tags;
using System;
using System.Collections.Generic;

namespace NetCore3WithReact.DAL.Entities.Sales
{
    public class Product: IdentityEntity
    {
        public string Name { get; set; }
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }
}
