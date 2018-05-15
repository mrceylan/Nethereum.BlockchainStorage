using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OpsICO.Core.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Edit(T entity);
    }
}