using System.Collections.Generic;
using InventoryManagement.Dreamer.Entitys;

namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class TransactionMetadata : Transaction
    {
        public TransactionMetadata()
        {
            Products = new List<StockMetaData>();
        }
        public List<StockMetaData> Products { get; set; }
    }
}