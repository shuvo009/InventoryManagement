using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Dreamer.Entitys.Interface;
namespace InventoryManagement.Dreamer.Entitys
{
    public class Brand : IEntity, ICommandEntity 
    {
        public Brand()
        {
            Productses = new HashSet<Product>();
        }

        [Key]
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Please Enter Brand Name.")]
        [MaxLength(100)]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Productses { get; set; }

        public Company Company { get; set; }

        [ForeignKey("Company")]
        public Int64 CompanyId { get; set; }
    }
}