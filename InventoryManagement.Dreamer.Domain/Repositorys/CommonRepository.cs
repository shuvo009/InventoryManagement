using System;
using System.Data.Entity;
using System.Linq;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Entitys.Interface;

namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class CommonRepository<T> : BasicRepository<T>, ICommonRepository<T> where T : class, ICommandEntity, IEntity, new()
    {
        public CommonRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public T GetById(Int64 id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

    }
}