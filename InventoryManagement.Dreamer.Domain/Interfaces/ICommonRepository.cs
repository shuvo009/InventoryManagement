using System;
using System.Linq;

namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface ICommonRepository<T> : IBasicRepository<T>
    {
        T GetById(Int64 id);
    }
}