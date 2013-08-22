using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagement.Dreamer.Domain.Interfaces;
using InventoryManagement.Dreamer.Entitys.Interface;
using System.Data.Entity;
namespace InventoryManagement.Dreamer.Domain.Repositorys
{
    public class BasicRepository<T> : IBasicRepository<T> where T : class,ICommandEntity, new()
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public BasicRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add((entity));
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public bool IsExists(Expression<Func<T, bool>> expression)
        {
            return DbSet.Any(expression);
        }
    }
}
