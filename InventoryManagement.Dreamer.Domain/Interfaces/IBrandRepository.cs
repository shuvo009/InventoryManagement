using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface IBrandRepository : ICommonRepository<Brand>
    {
        Brand GetBrandByCompanyId(Int64? companyId, string brandName);
    }
}
