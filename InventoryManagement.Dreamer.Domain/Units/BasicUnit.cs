using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Repositorys;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Domain.Context;
namespace InventoryManagement.Dreamer.Domain.Units
{
    public class BasicUnit : IBasicUnit
    {
        private readonly InventoryContext _inventoryContext;
        private ICommonRepository<Company> _companys;
        private IBrandRepository _brands;
        private IProductRepository _products;
        private ISkuInfoRepository _skus;
        private IStockRepository _stocks;
        private ITransction _transactions;
        private ITransctionHistory _transactionHistorys;

        public BasicUnit()
            : this(new InventoryContext())
        {

        }

        private BasicUnit(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }


        public void Dispose()
        {
            if (_inventoryContext != null)
                _inventoryContext.Dispose();
        }

        public ICommonRepository<Company> Companys
        {
            get { return _companys ?? (_companys = new CommonRepository<Company>(_inventoryContext)); }
        }

        public IBrandRepository Brands
        {
            get { return _brands ?? (_brands = new BrandRepository(_inventoryContext)); }
        }

        public IProductRepository Products
        {
            get { return _products ?? (_products = new ProductRepository(_inventoryContext)); }
        }

        public ISkuInfoRepository Skus
        {
            get { return _skus ?? (_skus = new SkuRepository(_inventoryContext)); }

        }

        public IStockRepository Stocks
        {
            get { return _stocks ?? (_stocks = new StockRepository(_inventoryContext)); }

        }

        public ITransction Transactions
        {
            get { return _transactions ?? (_transactions = new TransctionRepository(_inventoryContext)); }
        }

        public ITransctionHistory TransactionHistorys
        {
            get { return _transactionHistorys ?? (_transactionHistorys = new TransctionHistoryRepository(_inventoryContext)); }
        }

        public void Save()
        {
            _inventoryContext.SaveChanges();
        }
    }
}
