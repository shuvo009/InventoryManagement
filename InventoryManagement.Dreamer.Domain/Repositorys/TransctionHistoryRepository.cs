using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;

namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class TransctionHistoryRepository : CommonGuidRepository<TransactionHistory>,  ITransctionHistory
    {
        public TransctionHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<StockMetaData> GetTransctionHistorys(Expression<Func<TransactionHistory, bool>> expression)
        {
            return DbSet.Include(x => x.Company)
                        .Include(x => x.Brand)
                        .Include(x => x.Product)
                        .Include(x => x.Sku)
                        .Where(expression)
                        .Select(x => new StockMetaData
                            {
                                Amount = x.Amount,
                                BrandName = x.Brand.BrandName,
                                CompanyName = x.Company.CompanyName,
                                ProductName = x.Product.ProductName,
                                SkuName = x.Sku.SkuName,
                                Quantity = x.Quantity,
                                Rate = x.Rate
                            }).ToList();

        }
    }
}