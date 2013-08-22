using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface IStockRepository : ICommonRepository<Stock>
    {
        Stock GetStockBySkuId(Int64 id);
        IEnumerable<StockMetaData> StockInfo(Expression<Func<Stock, bool>> expression);
        SkuForTransactionMetadata SkuForTransactionMetadataById(Int64 skuId);
    }
}
