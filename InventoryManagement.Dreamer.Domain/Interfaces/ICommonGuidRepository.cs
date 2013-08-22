using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface ICommonGuidRepository<T> : IBasicRepository<T>
    {
        T GetById(Guid id);
    }
}