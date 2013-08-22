using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
using AutoMapper;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class SkuRepository : CommonRepository<Sku>, ISkuInfoRepository
    {
        public SkuRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        #region ISkuInfoRepository Members

        public SkuMetadata GetSkuById(long id)
        {
            var skuInfo = DbSet
                .Include(x => x.Product.Brand.Company)
                .FirstOrDefault(x => x.Id == id);
            if (skuInfo == null)
                return new SkuMetadata();

            return new SkuMetadata
                       {
                           CompanyId = skuInfo.Product.Brand.Company.Id,
                           CompanyName = skuInfo.Product.Brand.Company.CompanyName,
                           BrandId = skuInfo.Product.Brand.Id,
                           BrandName = skuInfo.Product.Brand.BrandName,
                           ProductId = skuInfo.Product.Id,
                           ProductName = skuInfo.Product.ProductName,
                           SkuId = skuInfo.Id,
                           SkuName = skuInfo.SkuName
                       };
        }

        public IEnumerable<SkuMetadata> GetAllSku()
        {
            return DbSet.Include(x => x.Product.Brand.Company).Where(x=>!x.IsDeleted).Select(x => new SkuMetadata
                {
                    CompanyName = x.Product.Brand.Company.CompanyName,
                    CompanyId = x.Product.Brand.Company.Id,
                    BrandId = x.Product.Brand.Id,
                    BrandName = x.Product.Brand.BrandName,
                    ProductId = x.Product.Id,
                    ProductName = x.Product.ProductName,
                    SkuId = x.Id,
                    SkuName = x.SkuName
                }).ToList();
        }

        public Sku GetSkuByCompanyBrandAndProduct(long? companyId, long? brandId, long? productId, string skuName)
        {
            if (!companyId.HasValue || !brandId.HasValue || !productId.HasValue)
                return null;

            return DbSet
               .Include(x => x.Product.Brand.Company)
               .FirstOrDefault(x => x.SkuName.ToLower().Equals(skuName.ToLower())
                                && x.Product.Brand.Company.Id == companyId 
                                && x.Product.Brand.Id == brandId
                                && x.Product.Id == productId);
        }

        #endregion
    }
}