using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Nethereum.BlockchainStore.Processors;
using Nethereum.BlockchainStore.Processors.PostProcessors;
using Nethereum.BlockchainStore.Processors.Transactions;
using Nethereum.BlockchainStore.Repositories;
using Nethereum.BlockchainStore.SQL;
using Nethereum.JsonRpc.IpcClient;
using NLog.Fluent;

namespace Nethereum.BlockchainStore.Processing.Console
{
  public class StorageProcessor
  {
    private const int MaxRetries = 3;
    private readonly Web3.Web3 _web3;
    private readonly IBlockProcessor _procesor;
    private int _retryNumber;

    public StorageProcessor(string url)
    {
      _web3 = url.EndsWith(".ipc") ? new Web3.Web3(new IpcClient(url)) : new Web3.Web3(url);

      var blockRepository = new BlockRepository();
      var transactionRepository = new TransactionRepository();
      var addressTransactionRepository = new AddressTransactionRepository();
      var contractRepository = new ContractRepository();
      var logRepository = new TransactionLogRepository();
      var vmStackRepository = new TransactionVMStackRepository();

      var contractTransactionProcessor = new ContractTransactionProcessor(_web3, contractRepository,
                transactionRepository, logRepository);
      var contractCreationTransactionProcessor = new ContractCreationTransactionProcessor(_web3, contractRepository,
          transactionRepository);
      var valueTrasactionProcessor = new ValueTransactionProcessor(transactionRepository);

      var transactionProcessor = new TransactionProcessor(_web3, contractTransactionProcessor,
          valueTrasactionProcessor, contractCreationTransactionProcessor);

      transactionProcessor.ContractTransactionProcessor.EnabledVmProcessing = false;
      _procesor = new BlockProcessor(_web3, blockRepository, transactionProcessor, transactionRepository);



    }

    public async Task Init()
    {
      await Contract.InitContractsCacheAsync().ConfigureAwait(false);
    }

    public async Task<bool> ExecuteAsync(int startBlock, int endBlock)
    {
      await Init();
      Stopwatch stopwatch = new Stopwatch();
      while (startBlock <= endBlock)
      {
        try
        {
          stopwatch.Reset();
          stopwatch.Start();
          await _procesor.ProcessBlockAsync(startBlock).ConfigureAwait(false);
          _retryNumber = 0;
          stopwatch.Stop();
          System.Console.WriteLine("Geçen süre : " + stopwatch.Elapsed.TotalSeconds);
          System.Console.WriteLine("");
          System.Console.WriteLine("---------------------------------------------");
          System.Console.WriteLine("");
          if (startBlock.ToString().EndsWith("0"))
            System.Console.WriteLine(startBlock + " " + DateTime.Now.ToString("s"));


          startBlock = startBlock + 1;
        }
        catch (Exception ex)
        {
          if (ex.StackTrace.Contains("Only one usage of each socket address"))
          {
            Thread.Sleep(1000);
            System.Console.WriteLine("SOCKET ERROR:" + startBlock + " " + DateTime.Now.ToString("s"));
            await ExecuteAsync(startBlock, endBlock).ConfigureAwait(false);
          }
          else
          {
            if (_retryNumber != MaxRetries)
            {
              _retryNumber = _retryNumber + 1;
              await ExecuteAsync(startBlock, endBlock).ConfigureAwait(false);
            }
            else
            {
              startBlock = startBlock + 1;
              Log.Error().Exception(ex).Message("BlockNumber" + startBlock).Write();
              System.Console.WriteLine("ERROR:" + startBlock + " " + DateTime.Now.ToString("s"));
            }
          }
        }
      }
      return true;
    }
  }
}