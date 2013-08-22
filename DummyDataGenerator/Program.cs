using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Domain.Units;
using InventoryManagement.Dreamer.Entitys;
namespace DummyDataGenerator
{
    class Program
    {
        private static BasicUnit _basicUnit;

        static void Main(string[] args)
        {
            _basicUnit = new BasicUnit();
            CreateCompany(5);
            CreateBrand(5);
            CreateProducts(5);
            CreateSkus(10);
            PurchaseProducts(5);
            SalesProducts(5);
            Console.ReadKey();
        }

        static void CreateCompany(int amount)
        {
            Console.WriteLine("Inserting Company >> \n");
            foreach (var number in Enumerable.Range(1, amount))
            {
                _basicUnit.Companys.Add(new Company
                                            {
                                                CompanyName = string.Format("Company_{0}", number)
                                            });
                Console.Write("\r Inserting Company >> {0}", number);
            }
            Console.WriteLine("\nSave Companys");
            _basicUnit.Save();
        }

        static void CreateBrand(int amountPerCompany)
        {
            Console.WriteLine("Getting All companys");
            foreach (var company in _basicUnit.Companys.GetAll())
            {
                Console.WriteLine("Inserting Brand at {0} \n", company.CompanyName);
                foreach (var number in Enumerable.Range(1, amountPerCompany))
                {
                    if (company.Brandses == null)
                        company.Brandses = new Collection<Brand>();

                    company.Brandses.Add(new Brand
                                             {
                                                 BrandName = string.Format("Brand_{0}", number)
                                             });
                    Console.Write("\r Inserting {0} Brand at {1}", amountPerCompany, company.CompanyName);
                }
            }
            Console.WriteLine("Save Brands");
            _basicUnit.Save();
        }

        static void CreateProducts(int amountPerBrand)
        {
            Console.WriteLine("Getting All companys");
            foreach (var company in _basicUnit.Companys.GetAll())
            {
                Console.WriteLine("Getting All Barnds from {0}", company.CompanyName);
                foreach (var brand in company.Brandses)
                {
                    Console.WriteLine("Inserting Product at {0}>>{1}\n", company.CompanyName, brand.BrandName);
                    foreach (var number in Enumerable.Range(1, amountPerBrand))
                    {
                        if (brand.Productses == null)
                            brand.Productses = new Collection<Product>();

                        brand.Productses.Add(new Product
                        {
                            ProductName = string.Format("Product_{0}", number)
                        });
                        Console.Write("\rInserting {2} Product at {0}>>{1}", company.CompanyName, brand.BrandName, number);
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Save Products");
            _basicUnit.Save();

        }

        static void CreateSkus(int amountPerProduct)
        {
            Console.WriteLine("Getting All companys");
            foreach (var company in _basicUnit.Companys.GetAll())
            {
                Console.WriteLine("Getting All Barnds from {0}", company.CompanyName);
                foreach (var brand in company.Brandses)
                {
                    Console.WriteLine("Getting All Products from {0}>>{1}", company.CompanyName, brand.BrandName);
                    foreach (var product in brand.Productses)
                    {
                        Console.WriteLine("Inserting 1 Sku at {0}>>{1}>>{2} \n", company.CompanyName, brand.BrandName, product.ProductName);
                        foreach (var number in Enumerable.Range(1, amountPerProduct))
                        {
                            if (product.Skuses == null)
                                product.Skuses = new Collection<Sku>();
                            var skuInfo = new Sku
                                              {
                                                  SkuName = string.Format("Sku_{0}", number)
                                              };
                            product.Skuses.Add(skuInfo);

                            _basicUnit.Stocks.Add(new Stock
                                                      {
                                                          BrandId = brand.Id,
                                                          CompnayId = company.Id,
                                                          ProductId = product.Id,
                                                          Quantity = 0,
                                                          Rate = 0,
                                                          Sku = skuInfo
                                                      });
                            Console.Write("\rInserting {2} SKu at {0}>>{1}>>{3}", company.CompanyName, brand.BrandName, number, product.ProductName);
                        }
                        Console.WriteLine("");
                    }
                }
            }
            Console.WriteLine("Save Skus");
            _basicUnit.Save();
        }

        static void PurchaseProducts(int amount)
        {
            Console.WriteLine("Generating Purchase Tarnction And Update Stock");
            var randomNumber = new Random();
            foreach (var item in Enumerable.Range(0, amount))
            {
                var transctionHistorys = new List<TransactionHistory>();
                foreach (var skus in _basicUnit.Skus.GetAll().OrderBy(x => Guid.NewGuid()).Take(5).ToList())
                {
                    var skuMetadata = _basicUnit.Skus.GetSkuById(skus.Id);
                    var quantity = randomNumber.Next(1,20);
                    var rate = Convert.ToDecimal(randomNumber.NextDouble() * randomNumber.Next(1,20));
                    var stockInfo = _basicUnit.Stocks.GetStockBySkuId(Convert.ToInt64(skuMetadata.SkuId));
                    if (stockInfo != null)
                    {
                        stockInfo.Rate = rate;
                        stockInfo.Quantity += quantity;
                    }
                    transctionHistorys.Add(new TransactionHistory
                                                {
                                                    CompnayId = Convert.ToInt64(skuMetadata.CompanyId),
                                                    BrandId = Convert.ToInt64(skuMetadata.BrandId),
                                                    ProductId = Convert.ToInt64(skuMetadata.ProductId),
                                                    SkuId = Convert.ToInt64(skuMetadata.SkuId),
                                                    Quantity = quantity,
                                                    Rate = rate,
                                                    Amount = quantity * rate
                                                });
                }
                _basicUnit.Transactions.Add(new Transaction
                                                {
                                                    InvoiceNumber = GetRandomString(5),
                                                    IsSales = false,
                                                    PurchaseAmount = transctionHistorys.Sum(x => x.Amount),
                                                    TransactionDate = DateTime.Today.AddDays(randomNumber.Next(-50, 50)),
                                                    TransactionHistories = transctionHistorys
                                                });
            }
            Console.WriteLine("Saving Tarnction (Purchase)");
            _basicUnit.Save();
        }

        static void SalesProducts(int amount)
        {
            Console.WriteLine("Generating Sales Tarnction And Update Stock");
            var randomNumber = new Random();
            foreach (var item in Enumerable.Range(0, amount))
            {
                var transctionHistorys = new List<TransactionHistory>();
                foreach (var skus in _basicUnit.Skus.GetAll().OrderBy(x => Guid.NewGuid()).Take(5).ToList())
                {
                    var skuMetadata = _basicUnit.Skus.GetSkuById(skus.Id);
                    var quantity = randomNumber.Next(1,10);
                    var stockInfo = _basicUnit.Stocks.GetStockBySkuId(Convert.ToInt64(skuMetadata.SkuId));

                    if (stockInfo != null)
                    {
                        if (stockInfo.Quantity < quantity)
                            break;

                        stockInfo.Quantity -= quantity;
                    }
                    transctionHistorys.Add(new TransactionHistory
                    {
                        CompnayId = Convert.ToInt64(skuMetadata.CompanyId),
                        BrandId = Convert.ToInt64(skuMetadata.BrandId),
                        ProductId = Convert.ToInt64(skuMetadata.ProductId),
                        SkuId = Convert.ToInt64(skuMetadata.SkuId),
                        Quantity = quantity,
                        Rate = stockInfo.Rate,
                        Amount = quantity * stockInfo.Rate
                    });
                }
                _basicUnit.Transactions.Add(new Transaction
                {
                    InvoiceNumber = GetRandomString(5),
                    IsSales = true,
                    PurchaseAmount = transctionHistorys.Sum(x => x.Amount),
                    TransactionDate = DateTime.Today.AddDays(randomNumber.Next(-50, 50)),
                    TransactionHistories = transctionHistorys
                });
            }
            Console.WriteLine("Saving Tarnction (Sales)");
            _basicUnit.Save();
        }

        static string GetRandomString(int length)
        {
            var r = new Random();
            return new String(Enumerable.Range(0, length).Select(n => (Char)(r.Next(32, 127))).ToArray());
        }
    }
}
