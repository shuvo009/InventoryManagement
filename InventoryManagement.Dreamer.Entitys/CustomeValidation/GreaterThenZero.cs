using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace InventoryManagement.Dreamer.Entitys.CustomeValidation
{
    public class GreaterThenZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var isValid = false;

                if (value is int)
                    if (Convert.ToInt32(value) > 0) isValid = true;

                if (value is decimal)
                    if (Convert.ToDecimal(value) > 0) isValid = true;

                return isValid;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
