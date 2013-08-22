using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Dreamer.Entitys.CustomeValidation;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Entitys
{
    public class TransactionHistory : ITransctionEntity, ICommandEntity 
    {
        public TransactionHistory()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [DisplayName("Quantity")]
        [GreaterThenZero]
        public int Quantity { get; set; }

        [DisplayName("Rate")]
        public decimal Rate { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

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

        [ForeignKey("Transaction")]
        public Guid TransctionId { get; set; }

        public Transaction Transaction { get; set; }
    }
}
