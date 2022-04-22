using System.ComponentModel.DataAnnotations;

namespace NetCore3WithReact.DAL.Models.Sales
{
    public class Product: IdentityModel
    {
        public string Name { get; set; }        
        public Vendor Vendor { get; set; }
    }
}
