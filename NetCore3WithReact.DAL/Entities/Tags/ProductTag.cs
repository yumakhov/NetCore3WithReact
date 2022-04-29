using NetCore3WithReact.DAL.Entities.Sales;
using System;

namespace NetCore3WithReact.DAL.Entities.Tags
{
    public class ProductTag
    {
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
