using Nethereum.BlockchainStore.SQL.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Nethereum.BlockchainStore.SQL
{
  public class Contract
  {
    private static List<Contract> cachedContracts;

    public Contract()
    {

    }

    [Key]
    public string Address
    {
      get; set;
    }

    public string Name
    {
      get; set;
    }

    public string ABI
    {
      get; set;
    }

    public string Code
    {
      get; set;
    }

    public string Code1
    {
      get; set;
    }

    public string Code2
    {
      get; set;
    }

    public string Code3
    {
      get; set;
    }

    public string Code5
    {
      get; set;
    }

    public string Code6
    {
      get; set;
    }

    public string Code7
    {
      get; set;
    }

    public string Code8
    {
      get; set;
    }

    public string Code9
    {
      get; set;
    }

    public string Code10
    {
      get; set;
    }

    public string Creator
    {
      get; set;
    }

    public string TransactionHash
    {
      get; set;
    }

    public static Contract CreateContract(string contractAddress, string code,
        RPC.Eth.DTOs.Transaction transactionSource)
    {
      var contract = new Contract()
      {
        Address = contractAddress,
        Creator = transactionSource.From,
        TransactionHash = transactionSource.TransactionHash
      };
      contract.InitCode(code);

      cachedContracts?.Add(contract);

      return contract;
    }

    public void InitCode(string code)
    {
      var codeArray = SplitByLength(code, 31000).ToArray();
      var max = codeArray.Length > 11 ? 11 : codeArray.Length;

      for (var i = 0; i < max; i++)
      {
        var property = i == 0 ? GetType().GetProperty("Code") : GetType().GetProperty("Code" + i);
        property.SetValue(this, codeArray[i]);
      }
    }

    public static IEnumerable<string> SplitByLength(string s, int length)
    {
      for (var i = 0; i < s.Length; i += length)
        if (i + length <= s.Length)
          yield return s.Substring(i, length);
        else
          yield return s.Substring(i);
    }

    public static async Task<Contract> FindAsync(string contractAddress)
    {
      if (cachedContracts != null) return cachedContracts.FirstOrDefault(x => x.Address == contractAddress);

      var tr = await new BlockchainStoreContext().Contracts.FindAsync(new System.Threading.CancellationToken(), contractAddress);
      if (tr != null)
        return tr;

      return null;
    }

    public static async Task InitContractsCacheAsync()
    {
      if (cachedContracts != null)
        cachedContracts = await FindAllAsync().ConfigureAwait(false);
    }

    public static async Task<List<Contract>> FindAllAsync()
    {
      BlockchainStoreContext tableQuery = new BlockchainStoreContext();
      var contracts = new List<Contract>();
      contracts = tableQuery.Contracts.ToList();
      return contracts;
    }

    public static async Task<bool> ExistsAsync(string contractAddress)
    {
      var contract = await new BlockchainStoreContext().Contracts.FindAsync(new System.Threading.CancellationToken(), contractAddress);
      if (contract != null) return true;
      return false;
    }
  }
}