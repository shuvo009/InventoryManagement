using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Dreamer.Domain.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace InventoryManagement.Dreamer.Web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly IBasicUnit _basicUnit;

        public CompanyController(IBasicUnit basicUnit)
        {
            _basicUnit = basicUnit;
        }

        //
        // GET: /Company/

        public ActionResult CompanyHome()
        {
            return View();
        }

        public ActionResult GetAllCompanys([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_basicUnit.Companys.GetAll().ToDataSourceResult(request));
        }

    }
}
