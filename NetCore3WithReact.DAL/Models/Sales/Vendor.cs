using System.Collections.Generic;

namespace NetCore3WithReact.DAL.Models.Sales
{
    public class Vendor: IdentityModel
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
