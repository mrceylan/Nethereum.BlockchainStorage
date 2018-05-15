using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OpsICO.Core.Data;
using OpsICO.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpsICO.Core.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                return;
            }
            else
            {
                Delete(entity);
            }
        }

        public virtual void Edit(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}