using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace InventoryManagement.Dreamer.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBasicUnit _basicUnit;
        //
        // GET: /Product/
        public ProductController(IBasicUnit basicUnit)
        {
            _basicUnit = basicUnit;
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult GetAllProducts([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_basicUnit.Skus.GetAllSku().ToDataSourceResult(request));
        }

        public JsonResult GetAllCompanys()
        {
            return Json(_basicUnit.Companys.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllBrandsByCompany(string id)
        {
            Int64 companyId;
            return !Int64.TryParse(id, out companyId) ? new JsonResult() : Json(_basicUnit.Brands.SearchFor(x => x.CompanyId == companyId).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProductsByBrand(string id)
        {
            Int64 brandId;
            return !Int64.TryParse(id, out brandId) ? new JsonResult() : Json(_basicUnit.Products.SearchFor(x => x.BrandId == brandId).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSkusByProduct(string id)
        {
            Int64 productId;
            return !Int64.TryParse(id, out productId) ? new JsonResult() : Json(_basicUnit.Skus.SearchFor(x => x.ProductId == productId).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewSku()
        {
            return View("_NewSku");
        }

        [HttpPost]
        public JsonResult NewSku(SkuMetadata skuMetadata)
        {
            ModelState.Remove("CompanyId");
            ModelState.Remove("BrandId");
            ModelState.Remove("ProductId");
            ModelState.Remove("SkuId");

            if (!ModelState.IsValid)
                return Json(new AjaxResult
                    {
                        IsError = true,
                        ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).ToList()
                    });

            var newCompany = new Company { CompanyName = skuMetadata.CompanyName };
            var newBrand = new Brand { BrandName = skuMetadata.BrandName };
            var newProduct = new Product { ProductName = skuMetadata.ProductName };
            var newSku = new Sku { SkuName = skuMetadata.SkuName };
            var stcok = new Stock();

            var companyInfo =
                _basicUnit.Companys.SearchFor(x => x.CompanyName.ToLower().Equals(skuMetadata.CompanyName.ToLower()))
                          .FirstOrDefault();
            var brandInfo =
                _basicUnit.Brands.GetBrandByCompanyId(companyInfo != null ? companyInfo.Id : (long?)null,
                                                      skuMetadata.BrandName);
            var productInfo =
                _basicUnit.Products.GetProductByComandAndBrandId(companyInfo != null ? companyInfo.Id : (long?)null,
                                                                 brandInfo != null ? brandInfo.Id : (long?)null,
                                                                 skuMetadata.ProductName);
            var skuInfo =
                _basicUnit.Skus.GetSkuByCompanyBrandAndProduct(companyInfo != null ? companyInfo.Id : (long?)null,
                                                               brandInfo != null ? brandInfo.Id : (long?)null,
                                                               productInfo != null ? productInfo.Id : (long?)null,
                                                               skuMetadata.SkuName);

            if (companyInfo != null && brandInfo != null && productInfo != null && skuInfo != null)
            {
                var ajaxresult = new AjaxResult
                    {
                        IsError = true
                    };
                ajaxresult.ErrorMessages.Add(new ModelError("Product Already Exist."));
                return Json(ajaxresult);
            }

            if (companyInfo == null)
            {
                newSku.Product = newProduct;
                newProduct.Brand = newBrand;
                newBrand.Company = newCompany;
                stcok.Company = newCompany;
                stcok.Product = newProduct;
                stcok.Brand = newBrand;
                stcok.Sku = newSku;
            }
            else
            {
                if (brandInfo == null)
                {
                    newSku.Product = newProduct;
                    newProduct.Brand = newBrand;
                    newBrand.Company = companyInfo;
                    stcok.Company = companyInfo;
                    stcok.Product = newProduct;
                    stcok.Brand = newBrand;
                    stcok.Sku = newSku;
                }
                else
                {
                    if (productInfo == null)
                    {
                        newSku.Product = newProduct;
                        newProduct.Brand = brandInfo;
                        brandInfo.Company = companyInfo;
                        stcok.Company = companyInfo;
                        stcok.Product = newProduct;
                        stcok.Brand = brandInfo;
                        stcok.Sku = newSku;
                    }
                    else
                    {
                        newSku.Product = productInfo;
                        productInfo.Brand = brandInfo;
                        brandInfo.Company = companyInfo;
                        stcok.Company = companyInfo;
                        stcok.Product = productInfo;
                        stcok.Brand = brandInfo;
                        stcok.Sku = newSku;
                    }
                }
            }
            _basicUnit.Skus.Add(newSku);
            _basicUnit.Stocks.Add(stcok);
            _basicUnit.Save();
            return Json(new AjaxResult
                {
                    IsError = false
                });
        }

        public ActionResult EditSku(Int64 id)
        {
            return View("_EditSku", _basicUnit.Skus.GetSkuById(id));
        }

        [HttpPost]
        public JsonResult EditSku(SkuMetadata skuMetadata)
        {
            if (!ModelState.IsValid)
                return Json(new AjaxResult
                {
                    IsError = true,
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).ToList()
                });

            if (skuMetadata.CompanyId != null)
            {
                var companyInfo = _basicUnit.Companys.GetById((Int64)skuMetadata.CompanyId);
                companyInfo.CompanyName = skuMetadata.CompanyName;
            }

            if (skuMetadata.BrandId != null)
            {
                var brandInfo = _basicUnit.Brands.GetById((Int64)skuMetadata.BrandId);
                brandInfo.BrandName = skuMetadata.BrandName;
            }

            if (skuMetadata.ProductId != null)
            {
                var productInfo = _basicUnit.Products.GetById((Int64)skuMetadata.ProductId);
                productInfo.ProductName = skuMetadata.ProductName;
            }

            if (skuMetadata.SkuId != null)
            {
                var skuInfo = _basicUnit.Skus.GetById((Int64)skuMetadata.SkuId);
                skuInfo.SkuName = skuMetadata.SkuName;
            }

            _basicUnit.Save();

            return Json(new AjaxResult
            {
                IsError = false
            });
        }

        public ActionResult DeleteSku(Int64 id)
        {
            return View("_DeleteSku", _basicUnit.Skus.GetSkuById(id));
        }

        [HttpPost]
        public ActionResult DeleteSku(SkuMetadata skuMetadata)
        {
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

            if (skuMetadata.SkuId != null)
            {
                _basicUnit.Skus.Delete(_basicUnit.Skus.GetById((Int64)skuMetadata.SkuId));
                _basicUnit.Stocks.Delete(_basicUnit.Stocks.GetStockBySkuId((Int64)skuMetadata.SkuId));
            }
            _basicUnit.Save();

            return Json(new AjaxResult
            {
                IsError = false
            });
        }

    }
}
