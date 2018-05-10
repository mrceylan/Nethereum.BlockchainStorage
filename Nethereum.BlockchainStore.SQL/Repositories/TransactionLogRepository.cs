using System.Data.Entity;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL.Context;
using Newtonsoft.Json.Linq;


namespace Nethereum.BlockchainStore.SQL
{
  public class TransactionLogRepository : ITransactionLogRepository 
  {


    public TransactionLogRepository()
    {

    }

    //public async Task UpsertAsync(string transactionHash, long logIndex,
    //    JObject log)
    //{
    //  var entity = TransactionLog.CreateTransactionLog(transactionHash,
    //      logIndex, log);

    //  await InsertOrUpdate(entity);
    //}

    public async Task InsertOrUpdate(TransactionLog trxlog)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(trxlog).State = trxlog.LogIndex == 0 ?
        //                           EntityState.Added :
        //                           EntityState.Modified;

        try
        {
          context.TransactionLogs.Add(trxlog);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {

        }
      }
    }
  }
}