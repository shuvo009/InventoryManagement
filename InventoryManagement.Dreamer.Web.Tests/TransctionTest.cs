using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Domain.Units;
using InventoryManagement.Dreamer.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transaction = InventoryManagement.Dreamer.Web.Models.Transaction;

namespace InventoryManagement.Dreamer.Web.Tests
{
    [TestClass]
    public class TransctionTest
    {


        [TestMethod]
        public void TransctionCompleateTest()
        {
            var con = new BasicUnit();
            var tc = new Transaction(con);

            Stock skuinfo = con.Stocks.GetStockBySkuId(20);


            var tm = new TransactionMetadata
                {
                    IsSales = false,
                    InvoiceNumber = "123",
                    SalesAmount = 500,
                    TransactionDate = DateTime.Today,
                    Products = new List<StockMetaData>
                        {
                            new StockMetaData
                                {
                                    SkuId = skuinfo.SkuId,
                                    BrandId = skuinfo.BrandId,
                                    CompanyId = skuinfo.CompnayId,
                                    Amount =  200,
                                    ProductId = skuinfo.ProductId,
                                    Quantity = 10,
                                    Rate = 5
                                }
                        }
                };


            tc.TransctionCompleate(tm);

            Stock skuinfoAfterTest = con.Stocks.GetStockBySkuId(20);

            Assert.AreEqual(skuinfo.Quantity - 10, skuinfoAfterTest.Quantity);

        }
    }
}
