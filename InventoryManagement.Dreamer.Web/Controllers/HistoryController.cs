using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace InventoryManagement.Dreamer.Web.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly IBasicUnit _basicUnit;

        public HistoryController(IBasicUnit basicUnit)
        {
            _basicUnit = basicUnit;
        }

        public ActionResult Purchase()
        {
            var searchMetadata = new TranctionSearchMetadata { IsSales = false };
            return View("TransctionHistory",searchMetadata);
        }

        public ActionResult Sales()
        {
            var searchMetadata = new TranctionSearchMetadata { IsSales = true };
            return View("TransctionHistory", searchMetadata);
        }

        public ActionResult GetTransctions(TranctionSearchMetadata tranctionSearchMetadata, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(_basicUnit.Transactions.GetTransctions(x => tranctionSearchMetadata.From <= x.TransactionDate
                                                               && tranctionSearchMetadata.To >= x.TransactionDate
                                                               && x.IsSales == tranctionSearchMetadata.IsSales)
                                  .ToDataSourceResult(request));
        }

        public ActionResult GetTransctionHistory(Guid transctionId, [DataSourceRequest] DataSourceRequest request)
        {

            return Json(_basicUnit.TransactionHistorys.GetTransctionHistorys(x=>!x.IsDeleted && x.TransctionId == transctionId)
                                  .ToDataSourceResult(request));
        }
    }
}
