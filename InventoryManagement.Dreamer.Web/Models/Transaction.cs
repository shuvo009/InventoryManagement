using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Web.Models.Interface;
namespace InventoryManagement.Dreamer.Web.Models
{
    public class Transaction : ITransaction
    {
        private readonly IBasicUnit _basicUnit;

        public Transaction(IBasicUnit basicUnit)
        {
            _basicUnit = basicUnit;
        }

        public bool TransctionCompleate(TransactionMetadata transactionMetadata)
        {
            var transaction = new Entitys.Transaction
                {
                    InvoiceNumber = transactionMetadata.InvoiceNumber,
                    TransactionDate = transactionMetadata.TransactionDate,
                    IsSales = transactionMetadata.IsSales
                };
            var totalAmount = transactionMetadata.Products.Sum(x => x.Amount);
            if (transactionMetadata.IsSales)
                transaction.SalesAmount = totalAmount;
            else
                transaction.PurchaseAmount = totalAmount;
            foreach (var product in transactionMetadata.Products)
            {
                var skuId = Convert.ToInt64(product.SkuId);
                var transctionHistory = new TransactionHistory
                    {
                        Amount = product.Amount,
                        BrandId = Convert.ToInt64(product.BrandId),
                        CompnayId = Convert.ToInt64(product.CompanyId),
                        ProductId = Convert.ToInt64(product.ProductId),
                        Quantity = product.Quantity,
                        Rate = product.Rate,
                        SkuId = skuId,
                        Transaction = transaction
                    };
                _basicUnit.TransactionHistorys.Add(transctionHistory);
                var stockInfo = _basicUnit.Stocks.GetStockBySkuId(skuId);
                if (transactionMetadata.IsSales)
                    stockInfo.Quantity -= product.Quantity;
                else
                    stockInfo.Quantity += product.Quantity;
                stockInfo.Rate = product.Rate;
            }
            _basicUnit.Transactions.Add(transaction);

            try
            {
                _basicUnit.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public KeyValuePair<bool, TransactionMetadata> AddSkuToInvoice(TransactionMetadata transactionMetadata, SkuForTransactionMetadata skuForTransactionMetadata)
        {
            if (transactionMetadata.Products == null)
                transactionMetadata.Products = new List<StockMetaData>();

            var skuFortansaction = transactionMetadata.Products.FirstOrDefault(x => x.SkuId == skuForTransactionMetadata.SkuId);
            if (skuFortansaction != null)
            {
                if (transactionMetadata.IsSales)
                {
                    if (skuForTransactionMetadata.SkuId != null)
                    {
                        var stackInfo = _basicUnit.Stocks.GetStockBySkuId((Int64)skuForTransactionMetadata.SkuId);
                        if ((skuFortansaction.Quantity + skuForTransactionMetadata.TransactionQuantity) > stackInfo.Quantity)
                        {
                            return new KeyValuePair<bool, TransactionMetadata>(false, transactionMetadata);
                        }
                    }
                }

                skuFortansaction.Quantity += skuForTransactionMetadata.TransactionQuantity;
                skuForTransactionMetadata.Rate = skuForTransactionMetadata.TransactionRate;
                skuForTransactionMetadata.Amount = skuForTransactionMetadata.TransactionAmount;
                return new KeyValuePair<bool, TransactionMetadata>(true, transactionMetadata);

            }
            if (skuForTransactionMetadata.SkuId != null)
            {
                var skuInfo = _basicUnit.Skus.GetSkuById((Int64)skuForTransactionMetadata.SkuId);

                transactionMetadata.Products.Add(new StockMetaData
                    {
                        Amount = skuForTransactionMetadata.TransactionAmount,
                        BrandId = skuInfo.BrandId,
                        SkuName = skuInfo.SkuName,
                        BrandName = skuInfo.BrandName,
                        CompanyId = skuInfo.CompanyId,
                        CompanyName = skuInfo.CompanyName,
                        ProductId = skuInfo.ProductId,
                        ProductName = skuInfo.ProductName,
                        Quantity = skuForTransactionMetadata.TransactionQuantity,
                        Rate = skuForTransactionMetadata.TransactionRate,
                        SkuId = skuForTransactionMetadata.SkuId
                    });
                return new KeyValuePair<bool, TransactionMetadata>(true, transactionMetadata);
            }
            return new KeyValuePair<bool, TransactionMetadata>(false, transactionMetadata);            
        }
    }
}