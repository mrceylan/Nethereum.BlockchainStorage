using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpsICO.Core.Entities;
using OpsICO.Core.Repositories;
using OpsICO.Core.Repositories.Interfaces;

namespace OpsICO.Core.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException("dbcontext can not be null");
        }

        public ApplicationDbContext GetDbContext()
        {
            return context;
        }

        List<RepositoryListItem> RepositoryList = new List<RepositoryListItem>();

        public IRepository<T> GetRepository<T>() where T : class
        {
            var repositoryItem = RepositoryList.Where(q => q.Name == typeof(T).Name).FirstOrDefault();
            if (repositoryItem == null)
            {
                repositoryItem = new RepositoryListItem()
                {
                    Name = typeof(T).Name,
                    Repository = new EFRepository<T>(context)
                };
                RepositoryList.Add(repositoryItem);
            }
            return repositoryItem.Repository;
        }

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public void Commit()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}