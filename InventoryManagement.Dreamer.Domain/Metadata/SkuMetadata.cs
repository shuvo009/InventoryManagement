using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class SkuMetadata
    {
        public Int64? CompanyId { get; set; }

        [DisplayName("Company :")]
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string CompanyName { get; set; }

        public Int64? BrandId { get; set; }

        [DisplayName("Brand :")]
        [Required(ErrorMessage = "Please Enter Brand Name")]
        public string BrandName { get; set; }

        public Int64? ProductId { get; set; }

        [DisplayName("Product :")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string ProductName { get; set; }

        public Int64? SkuId { get; set; }

        [DisplayName("Sku :")]
        [Required(ErrorMessage = "Please Enter Sku Name")]
        public string SkuName { get; set; }
    }
}