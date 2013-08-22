using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Interfaces
{
   public interface ITransctionHistory : ICommonGuidRepository<TransactionHistory>
   {
       IEnumerable<StockMetaData> GetTransctionHistorys(Expression<Func<TransactionHistory, bool>> expression);
   }
}
