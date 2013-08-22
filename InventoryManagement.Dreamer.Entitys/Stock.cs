using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Dreamer.Entitys.CustomeValidation;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Entitys
{
    public class Stock : IEntity, ICommandEntity 
    {
        [Key]
        public Int64 Id { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DisplayName("Rate")]
        public decimal Rate { get; set; }

        public string Note { get; set; }

        public Company Company { get; set; }

        [ForeignKey("Company")]
        public Int64 CompnayId { get; set; }

        public Brand Brand { get; set; }

        [ForeignKey("Brand")]
        public Int64 BrandId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public Int64 ProductId { get; set; }

        public Sku Sku { get; set; }

        [ForeignKey("Sku")]
        public Int64 SkuId { get; set; }

        public bool IsDeleted { get; set; }
    }
}