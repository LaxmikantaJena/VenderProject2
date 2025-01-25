using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMarketing.Models
{
    public class VegetableVendor
    {
        [Key]
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VegetableName { get; set; }
        public decimal PricePerKg { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
