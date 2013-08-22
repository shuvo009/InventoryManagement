using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Entitys
{
    public class Product : IEntity, ICommandEntity 
    {
        public Product()
        {
            Skuses = new HashSet<Sku>();
        }

        [Key]
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name")]
        [MaxLength(100)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Sku> Skuses { get; set; }

        public Brand Brand { get; set; }

        [ForeignKey("Brand")]
        public Int64 BrandId { get; set; }
    }
}