using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.BlockchainStore.SQL.Context
{
  public class BlockchainStoreContext : DbContext 
  {

    public BlockchainStoreContext() : base("name=BlockChainConfig")
    {
      //Database.SetInitializer(new DropCreateDatabaseAlways<BlockchainStoreContext>());
    }


    public DbSet<AddressTransaction> AddressTransactions { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionLog> TransactionLogs { get; set; }
    public DbSet<TransactionVmStack> TransactionVmStacks { get; set; }
  }
}
