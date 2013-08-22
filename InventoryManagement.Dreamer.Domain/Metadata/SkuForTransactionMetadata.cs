using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using InventoryManagement.Dreamer.Entitys.CustomeValidation;
namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class SkuForTransactionMetadata :StockMetaData
    {
        [DisplayName("Quantity :")]
        [GreaterThenZero(ErrorMessage = "Please Enter Quantity.")]
        public int TransactionQuantity { get; set; }

        [DisplayName("Rate :")]
        [GreaterThenZero(ErrorMessage = "Please Enter Rate.")]
        public decimal TransactionRate { get; set; }
        
        [DisplayName("Amount :")]
        [GreaterThenZero(ErrorMessage = "Please Enter Amount.")]
        public decimal TransactionAmount { get; set; }
    }
}
