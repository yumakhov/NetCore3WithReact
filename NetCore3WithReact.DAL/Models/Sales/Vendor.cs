using System.ComponentModel.DataAnnotations;

namespace NetCore3WithReact.DAL.Models.Sales
{
    public class Vendor: IdentityModel
    {
        public string Name { get; set; }
    }
}
