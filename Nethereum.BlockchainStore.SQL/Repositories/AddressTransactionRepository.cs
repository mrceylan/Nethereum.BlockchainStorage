using System.Data.Entity;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL.Context;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.BlockchainStore.SQL
{
  public class AddressTransactionRepository /* : IAddressTransactionRepository */
  {
    public AddressTransactionRepository()
    {

    }

    //public async Task UpsertAsync(Nethereum.RPC.Eth.DTOs.Transaction transaction,
    //    TransactionReceipt transactionReceipt,
    //    bool failedCreatingContract,
    //    HexBigInteger blockTimestamp,
    //    string address,
    //    string error = null,
    //    bool hasVmStack = false,
    //    string newContractAddress = null)
    //{
    //  var entity = AddressTransaction.CreateAddressTransaction(transaction,
    //      transactionReceipt,
    //      failedCreatingContract, blockTimestamp, null, null, false, newContractAddress);
    //  await InsertOrUpdate(entity);
    //}

    public async Task InsertOrUpdate(AddressTransaction addr)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(addr).State = string.IsNullOrEmpty(addr.Address) ?
        //                           EntityState.Added :
        //                           EntityState.Modified;
        try
        {
          context.AddressTransactions.Add(addr);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {

        }
      }
    }
  }
}