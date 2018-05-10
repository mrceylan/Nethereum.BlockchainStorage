using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL.Context;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Transaction = Nethereum.RPC.Eth.DTOs.Transaction;

namespace Nethereum.BlockchainStore.SQL
{
  public class TransactionRepository : ITransactionRepository
  {
    public List<dynamic> TransactionList { get; set; }

    public TransactionRepository()
    {
      TransactionList = new List<dynamic>();
    }

    public async Task UpsertAsync(string contractAddress, string code, Nethereum.RPC.Eth.DTOs.Transaction transaction, TransactionReceipt transactionReceipt, bool failedCreatingContract, HexBigInteger blockTimestamp)
    {
      var transactionEntity = Nethereum.BlockchainStore.SQL.Transaction.CreateTransaction(
          transaction, transactionReceipt,
          failedCreatingContract, blockTimestamp, contractAddress);

      try
      {
        TransactionList.Add(transactionEntity);
      }
      catch (System.Exception e)
      {

        throw e;
      }
      //await InsertOrUpdate(transactionEntity);
    }

    public async Task UpsertAsync(Nethereum.RPC.Eth.DTOs.Transaction transaction,
      TransactionReceipt transactionReceipt,
        bool failed,
        HexBigInteger timeStamp, bool hasVmStack = false, string error = null)
    {
      var transactionEntity = Nethereum.BlockchainStore.SQL.Transaction.CreateTransaction(transaction,
          transactionReceipt,
          failed, timeStamp, hasVmStack, error);

      try
      {
        TransactionList.Add(transactionEntity);
      }
      catch (System.Exception e)
      {

        throw e;
      }
      //await InsertOrUpdate(transactionEntity);
    }

    public async Task AddTransactions()
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      using (var context = new BlockchainStoreContext())
      {
        try
        {
          context.Transactions.AddRange(TransactionList.Cast<Transaction>());
          await context.SaveChangesAsync();

          TransactionList = new List<dynamic>();
          stopwatch.Stop();
          System.Console.WriteLine("Transactionlarý VT ye yazma : " + stopwatch.Elapsed.TotalSeconds);
        }
        catch (System.Exception)
        {

        }

      }
    }

    public async Task InsertOrUpdate(Transaction transaction)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(transaction).State = string.IsNullOrEmpty(transaction.Hash) ?
        //                           EntityState.Added :
        //                           EntityState.Modified;

        try
        {
          context.Transactions.Add(transaction);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {

        }

      }
    }

  }
}