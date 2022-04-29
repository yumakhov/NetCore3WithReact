using System.Collections.Generic;

namespace NetCore3WithReact.DAL.Entities.Sales
{
    public class Vendor: IdentityEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
