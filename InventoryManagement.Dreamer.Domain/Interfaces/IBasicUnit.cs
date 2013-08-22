using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface IBasicUnit : IDisposable
    {
        ICommonRepository<Company> Companys { get; }
        IBrandRepository Brands { get; }
        IProductRepository Products { get; }
        ISkuInfoRepository Skus { get; }
        IStockRepository Stocks { get; }
        ITransction Transactions { get; }
        ITransctionHistory TransactionHistorys { get; }
        void Save();
    }
}
