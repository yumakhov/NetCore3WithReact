using System.ComponentModel.DataAnnotations;

namespace NetCore3WithReact.Models.Sales
{
    public class Product: Identity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Vendor Vendor { get; set; }
    }
}
