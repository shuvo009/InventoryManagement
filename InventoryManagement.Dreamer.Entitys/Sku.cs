using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Entitys
{
    public class Sku : IEntity, ICommandEntity 
    {
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Please Enter Sku Name")]
        [MaxLength(100)]
        [DisplayName("Sku Name")]
        public string SkuName { get; set; }

        public bool IsDeleted { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public Int64 ProductId { get; set; }
    }
}