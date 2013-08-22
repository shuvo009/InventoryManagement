using System;
using System.ComponentModel;

namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class TransctionHistoryMeteadata
    {
        public Guid TransctionId { get; set; }
        [DisplayName("Date")]
        public DateTime TransctionDate { get; set; }
        [DisplayName("Invoice Number")]
        public string InvoiceNumber { get; set; }
        public decimal? Amount { get; set; }
    }
}
