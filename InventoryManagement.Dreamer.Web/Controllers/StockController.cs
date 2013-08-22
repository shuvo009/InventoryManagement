using System.Web.Mvc;
using InventoryManagement.Dreamer.Domain.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace InventoryManagement.Dreamer.Web.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly IBasicUnit _basicUnit;

        public StockController(IBasicUnit basicUnit)
        {
            _basicUnit = basicUnit;
        }

        //
        // GET: /Stock/

        public ActionResult Stock()
        {
            return View();
        }

        public ActionResult GetAllSku([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_basicUnit.Stocks.StockInfo(x=>!x.IsDeleted).ToDataSourceResult(request));
        }
    }
}