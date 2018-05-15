using Nethereum.BlockChainStore.Data.Processors;
using OpsICO.Core.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Nethereum.BlockChainStore.Data
{
  class Program
  {
    static void Main(string[] args)
    {
      var url = "http://178.211.50.190:8545";
      //var url = "https://ropsten.infura.io/2riHiBOAVSxHOkL6DfLi";
      
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(@"Server=46.45.167.56; Database=opsdb;User ID=opsico;Password=JointOps.e1*.;");
      ApplicationDbContext con = new ApplicationDbContext(optionsBuilder.Options);
      UnitOfWork unitofwork = new UnitOfWork(con);

      var processor = new StorageProcessor(url, unitofwork);
      while (true)
      {
        new Helpers().AddLog(LogType.Info, "Data Yüklemesi Başlatılıyor");
        var result = processor.ExecuteAsync();
        Task.WaitAll(result);
      }

    }
  }
}
