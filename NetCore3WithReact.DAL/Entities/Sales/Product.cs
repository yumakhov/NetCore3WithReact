using System;

namespace NetCore3WithReact.DAL.Entities.Sales
{
    public class Product: IdentityModel
    {
        public string Name { get; set; }
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
