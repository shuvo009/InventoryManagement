using System.ComponentModel;

namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class StockMetaData : SkuMetadata
    {
        [DisplayName("Quantity :")]
        public int Quantity { get; set; }

        [DisplayName("Rate :")]
        public decimal Rate { get; set; }

        [DisplayName("Amount :")]
        public decimal Amount { get; set; }
    }
}