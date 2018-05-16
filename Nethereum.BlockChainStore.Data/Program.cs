
using OpsICO.Core.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Nethereum.BlockChainStore.Data.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;

namespace Nethereum.BlockChainStore.Data
{
  class Program
  {
    private static IUnitOfWork unitOfWork;
    static void Main(string[] args)
    {
      ServiceCollection serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
      IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

      var url = "http://localhost:8545";

      var processor = new StorageProcessor(url, unitOfWork);
      while (true)
      {
        new Helpers().AddLog(LogType.Info, "Processing Blockchain");
        var result = processor.ExecuteAsync();
        Task.WaitAll(result);
      }

    }

    private static void ConfigureServices(IServiceCollection services)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(@"Server=localhost; Database=BlockchainStore;Integrated Security=True;");
      ApplicationDbContext con = new ApplicationDbContext(optionsBuilder.Options);
      unitOfWork = new UnitOfWork(con);
    }
  }

  public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {

    ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseSqlServer(@"Server=localhost; Database=BlockchainStore;Integrated Security=True;");
      ApplicationDbContext con = new ApplicationDbContext(optionsBuilder.Options);
      return con;
    }
  }
}
