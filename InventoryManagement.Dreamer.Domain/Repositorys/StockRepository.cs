using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
using AutoMapper;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class StockRepository : CommonRepository<Stock>, IStockRepository
    {
        public StockRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public Stock GetStockBySkuId(long id)
        {
            return DbSet.FirstOrDefault(x => x.SkuId == id);
        }

        public IEnumerable<StockMetaData> StockInfo(Expression<Func<Stock, bool>> expression)
        {
            return DbSet
                .Include(x => x.Company)
                .Include(x => x.Brand)
                .Include(x => x.Product)
                .Include(x => x.Sku)
                .Where(expression)
                .OrderByDescending(x=>x.Quantity)
                .Select(x => new StockMetaData
                    {
                        BrandId = x.BrandId,
                        BrandName = x.Brand.BrandName,
                        CompanyId = x.CompnayId,
                        CompanyName = x.Company.CompanyName,
                        ProductId = x.ProductId,
                        ProductName = x.Product.ProductName,
                        Quantity = x.Quantity,
                        Rate = x.Rate,
                        SkuName = x.Sku.SkuName,
                        SkuId = x.SkuId
                    }).ToList();
        }

        public SkuForTransactionMetadata SkuForTransactionMetadataById(long skuId)
        {
            var stockInfo = StockInfo(x => x.SkuId == skuId).FirstOrDefault();
            Mapper.CreateMap<StockMetaData, SkuForTransactionMetadata>();
            return Mapper.Map<StockMetaData, SkuForTransactionMetadata>(stockInfo);
        }
    }
}
