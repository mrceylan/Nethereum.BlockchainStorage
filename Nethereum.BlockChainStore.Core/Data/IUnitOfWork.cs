using OpsICO.Core.Entities;
using OpsICO.Core.Repositories;
using OpsICO.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpsICO.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task CommitAsync();
        void Commit();
        ApplicationDbContext GetDbContext();
    }
}