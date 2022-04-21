using System.ComponentModel.DataAnnotations;

namespace NetCore3WithReact.DAL.Models.Sales
{
    public class Vendor: Identity
    {
        public string Name { get; set; }
    }
}
