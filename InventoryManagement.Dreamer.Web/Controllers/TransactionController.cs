using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Web.Models.Interface;
namespace InventoryManagement.Dreamer.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly IBasicUnit _basicUnit;
        private readonly ITransaction _transaction;

        public TransactionController(IBasicUnit basicUnit, ITransaction transaction)
        {
            _basicUnit = basicUnit;
            _transaction = transaction;
        }

        public ActionResult Purchase()
        {
            var transaction = GetTransactionSession();
            if (transaction.IsSales)
            {
                ResetSession();
                transaction = GetTransactionSession();
            }

            SetTransactionSession(transaction);
            return View("Transaction", transaction);
        }

        [HttpPost]
        public ActionResult TransactionCompleate(TransactionMetadata transactionMetadata, string command)
        {
            var transaction = GetTransactionSession();
            transaction.InvoiceNumber = transactionMetadata.InvoiceNumber;
            transaction.TransactionDate = transactionMetadata.TransactionDate;
            SetTransactionSession(transaction);
            switch (command)
            {
                case "Add SKU":
                    return RedirectToAction("SkuList");
                case "Save":
                    if (_transaction.TransctionCompleate(transaction))
                    {
                        ResetSession();
                        return RedirectToAction("Home", "Dashboard");
                    }
                    return View("Transaction", transactionMetadata);
                default:
                    ResetSession();
                    return RedirectToAction("Home", "Dashboard");
            }
        }

        public ActionResult Sales()
        {
            var transaction = GetTransactionSession(true);
            if (!transaction.IsSales)
            {
                ResetSession();
                transaction = GetTransactionSession(true);
            }
            SetTransactionSession(transaction);
            return View("Transaction", transaction);
        }

        public ActionResult SkuList()
        {
            var transactionInfo = GetTransactionSession();
            if (transactionInfo != null)
                ViewBag.RedirectState = transactionInfo.IsSales ? RedirectState.Sales : RedirectState.Purchase;
            else
                ViewBag.RedirectState = RedirectState.Dashboard;

            return View();
        }

        public ActionResult GetAllStock([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTransactionSession().IsSales ? _basicUnit.Stocks.StockInfo(x => !x.IsDeleted && x.Quantity > 0).ToDataSourceResult(request)
                                                        : _basicUnit.Stocks.StockInfo(x => !x.IsDeleted).ToDataSourceResult(request));
        }

        public ActionResult GetTransactionAbleSku([DataSourceRequest] DataSourceRequest request)
        {
            var transactionInfo = GetTransactionSession();
            return Json(transactionInfo.Products.ToDataSourceResult(request));
        }

        public ActionResult DeleteSkuFromTransction(Int64 skuId)
        {
            var transactionInfo = GetTransactionSession();
            var sku = transactionInfo.Products.FirstOrDefault(x => x.SkuId == skuId);
            return View("_DeleteSkuFromTransction", sku);
        }

        [HttpPost]
        public JsonResult DeleteSkuFromTransction(StockMetaData stockMetaData)
        {
            var transactionInfo = GetTransactionSession();
            var sku = transactionInfo.Products.FirstOrDefault(x => x.SkuId == stockMetaData.SkuId);
            if (sku != null)
            {
                transactionInfo.Products.Remove(sku);
                return Json(new AjaxResult
                {
                    IsError = false
                });
            }
            var productNotFountresult = new AjaxResult
            {
                IsError = true,
            };
            productNotFountresult.ErrorMessages.Add(new ModelError("SKU Is not Found."));
            return Json(productNotFountresult);
        }

        public ActionResult AddSku(Int64 skuId)
        {
            var skuInfo = _basicUnit.Stocks.SkuForTransactionMetadataById(skuId);
            ViewBag.IsSales = GetTransactionSession().IsSales;
            return View("_AddToTransction", skuInfo);
        }

        [HttpPost]
        public JsonResult AddSku(SkuForTransactionMetadata skuForTransactionMetadata)
        {
            ModelState.Remove("CompanyId");
            ModelState.Remove("BrandId");
            ModelState.Remove("ProductId");
            ModelState.Remove("CompanyName");
            ModelState.Remove("BrandName");
            ModelState.Remove("ProductName");
            ModelState.Remove("SkuName");
            if (!ModelState.IsValid)
                return Json(new AjaxResult
                {
                    IsError = true,
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).ToList()
                });
            var transactionInfo = GetTransactionSession();
            if (transactionInfo != null)
            {
                var cartInfo = _transaction.AddSkuToInvoice(transactionInfo, skuForTransactionMetadata);
                if (cartInfo.Key)
                {
                    SetTransactionSession(cartInfo.Value);
                }
                var ajaxresult = new AjaxResult
                {
                    IsError = !cartInfo.Key,
                };
                if (!cartInfo.Key)
                    ajaxresult.ErrorMessages.Add(new ModelError("Out of Stock."));
                return Json(ajaxresult);
            }
            var productNotFountresult = new AjaxResult
            {
                IsError = true,
            };
            productNotFountresult.ErrorMessages.Add(new ModelError("SKU Is not Found."));
            return Json(productNotFountresult);
        }

        private TransactionMetadata GetTransactionSession(bool isSales = false)
        {
            return Session["Transaction"] as TransactionMetadata ?? new TransactionMetadata() { IsSales = isSales };
        }

        private void SetTransactionSession(TransactionMetadata transactionMetadata)
        {
            Session["Transaction"] = transactionMetadata;
        }

        private void ResetSession()
        {
            Session["Transaction"] = null;
        }
    }
}
