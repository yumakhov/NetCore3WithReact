using System;

namespace NetCore3WithReact.DAL.Entities.Tags
{
    public class ProductTag: IdentityEntity
    {
        public Guid TagId { get; set; }
        public Guid ProductId { get; set; }
    }
}
