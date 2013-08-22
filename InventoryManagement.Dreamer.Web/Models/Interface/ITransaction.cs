using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Domain.Metadata;

namespace InventoryManagement.Dreamer.Web.Models.Interface
{
    public interface ITransaction
    {
        bool TransctionCompleate(TransactionMetadata transactionMetadata);
        KeyValuePair<bool, TransactionMetadata> AddSkuToInvoice(TransactionMetadata transactionMetadata, SkuForTransactionMetadata skuForTransactionMetadata);
    }
}
