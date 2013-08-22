using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Domain.Interfaces;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class BrandRepository : CommonRepository<Brand> ,IBrandRepository
    {
        public BrandRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Brand GetBrandByCompanyId(long? companyId, string brandName)
        {
            return !companyId.HasValue ? null : DbSet.FirstOrDefault(x => x.CompanyId == companyId && x.BrandName.ToLower().Equals(brandName.ToLower()));
        }
    }
}
