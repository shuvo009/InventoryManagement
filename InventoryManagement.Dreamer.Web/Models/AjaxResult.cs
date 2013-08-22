using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Dreamer.Web.Models
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            ErrorMessages = new List<ModelError>();
        }
        public bool IsError { get; set; }
        public List<ModelError> ErrorMessages{ get; set; }
    }
}