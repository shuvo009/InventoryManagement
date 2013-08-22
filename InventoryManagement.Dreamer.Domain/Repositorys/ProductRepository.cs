using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Domain.Interfaces;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public Product GetProductByComandAndBrandId(long? companyId, long? brandId, string productName)
        {
            if (!companyId.HasValue || !brandId.HasValue)
                return null;

            return
                DbSet.Include(x => x.Brand.Company)
                     .FirstOrDefault(x => x.ProductName.ToLower().Equals(productName.ToLower())
                                    && x.BrandId == brandId
                                    && x.Brand.Company.Id == companyId);
        }
    }
}
