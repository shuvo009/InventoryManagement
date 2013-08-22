using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Entitys
{
    public class Transaction : ITransctionEntity, ICommandEntity 
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
        }

        [DisplayName("Date")]
        public DateTime TransactionDate { get; set; }

        public decimal? SalesAmount { get; set; }

        public decimal? PurchaseAmount { get; set; }

        [Required(ErrorMessage = "Please Entre Invoice Number.")]
        [MaxLength(100)]
        [DisplayName("Invoice Number")]
        public string InvoiceNumber { get; set; }

        public bool IsSales { get; set; }

        public ICollection<TransactionHistory> TransactionHistories { get; set; }

        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}