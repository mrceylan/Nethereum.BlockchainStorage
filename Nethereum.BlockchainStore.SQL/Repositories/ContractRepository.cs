using System.Data.Entity;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL.Context;
using Transaction = Nethereum.RPC.Eth.DTOs.Transaction;

namespace Nethereum.BlockchainStore.SQL
{
  public class ContractRepository : IContractRepository
  {

    public ContractRepository()
    {
    }

    public async Task<bool> ExistsAsync(string contractAddress)
    {
      return await Contract.ExistsAsync(contractAddress).ConfigureAwait(false);
    }

    public async Task UpsertAsync(string contractAddress, string code, Nethereum.RPC.Eth.DTOs.Transaction transaction)
    {
      var contract = Contract.CreateContract(contractAddress, code,
          transaction);
      //await InsertOrUpdate(contract);
    }

    public async Task InsertOrUpdate(Contract contract)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(contract).State = string.IsNullOrEmpty(contract.Address) ?
        //                           EntityState.Added :
        //                           EntityState.Modified;

        try
        {
          context.Contracts.Add(contract);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {

        }
      }
    }
  }
}