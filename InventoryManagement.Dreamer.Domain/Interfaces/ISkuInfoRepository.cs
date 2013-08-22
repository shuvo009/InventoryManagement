using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Domain.Metadata;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface ISkuInfoRepository : ICommonRepository<Sku>
    {
        SkuMetadata GetSkuById(Int64 id);
        IEnumerable<SkuMetadata> GetAllSku();
        Sku GetSkuByCompanyBrandAndProduct(Int64? companyId, Int64? brandId, Int64? productId, string skuName);
    }
}
