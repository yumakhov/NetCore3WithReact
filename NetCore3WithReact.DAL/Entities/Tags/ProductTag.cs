using System;

namespace NetCore3WithReact.DAL.Entities.Tags
{
    public class ProductTag: IdentityModel
    {
        public Guid TagId { get; set; }
        public Guid ProductId { get; set; }
    }
}
