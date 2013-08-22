using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InventoryManagement.Dreamer.Entitys.Interface;
namespace InventoryManagement.Dreamer.Entitys
{
    public class Company : IEntity, ICommandEntity 
    {
        public Company()
        {
            Brandses = new HashSet<Brand>();
        }

        [Key]
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Please Enter Company Name")]
        [MaxLength(100)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Brand> Brandses { get; set; }
    }
}