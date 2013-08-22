using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
using InventoryManagement.Dreamer.Domain.Repositorys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface ITransction : ICommonGuidRepository<Transaction>
    {
        IEnumerable<TransctionHistoryMeteadata> GetTransctions(Expression<Func<Transaction, bool>> expression);
    }
}
