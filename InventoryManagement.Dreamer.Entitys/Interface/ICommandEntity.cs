using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagement.Dreamer.Entitys.Interface
{
    public interface ICommandEntity 
    {
         bool IsDeleted { get; set; }
    }
}
