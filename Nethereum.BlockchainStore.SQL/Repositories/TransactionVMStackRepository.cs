using System.Data.Entity;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL.Context;
using Newtonsoft.Json.Linq;


namespace Nethereum.BlockchainStore.SQL
{
  public class TransactionVMStackRepository /*: ITransactionVMStackRepository */
  {

    public TransactionVMStackRepository()
    {
    }

    //public async Task UpsertAsync(string transactionHash,
    //    string address,
    //    JObject stackTrace)
    //{
    //  var entity = TransactionVmStack.CreateTransactionVmStack(transactionHash, address, stackTrace);
    //  await InsertOrUpdate(entity);
    //}

    public async Task InsertOrUpdate(TransactionVmStack stack)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(stack).State = string.IsNullOrEmpty(stack.TransactionHash) ?
        //                           EntityState.Added :
        //                           EntityState.Modified;

        try
        {
          context.TransactionVmStacks.Add(stack);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {

        }
      }
    }
  }
}