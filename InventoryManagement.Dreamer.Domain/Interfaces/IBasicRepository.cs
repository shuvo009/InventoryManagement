using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InventoryManagement.Dreamer.Domain.Interfaces
{
    public interface IBasicRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        bool IsExists(Expression<Func<T, bool>> expression);
    }
}
