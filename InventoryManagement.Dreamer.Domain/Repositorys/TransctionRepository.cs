using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys;

namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class TransctionRepository: CommonGuidRepository<Transaction>,  ITransction
    {
        public TransctionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<TransctionHistoryMeteadata> GetTransctions(Expression<Func<Transaction, bool>> expression)
        {
            return SearchFor(expression).Select(x => new TransctionHistoryMeteadata
                {
                    TransctionId = x.Id,
                    TransctionDate = x.TransactionDate,
                    Amount = x.IsSales ? x.SalesAmount : x.PurchaseAmount,
                    InvoiceNumber = x.InvoiceNumber
                }).ToList();
        }
    }
}