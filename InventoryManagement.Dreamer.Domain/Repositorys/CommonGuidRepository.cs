
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Domain.Metadata;
using InventoryManagement.Dreamer.Entitys.Interface;
using InventoryManagement.Dreamer.Entitys;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class CommonGuidRepository<T> : BasicRepository<T>, ICommonGuidRepository<T>
        where T : class, ITransctionEntity, ICommandEntity, new()
    {
        public CommonGuidRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public T GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        
    }
}