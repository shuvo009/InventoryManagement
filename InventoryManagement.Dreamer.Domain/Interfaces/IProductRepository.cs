using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface IProductRepository : ICommonRepository<Product>
    {
        Product GetProductByComandAndBrandId(Int64? companyId, Int64? brandId, string productName);
    }
}
