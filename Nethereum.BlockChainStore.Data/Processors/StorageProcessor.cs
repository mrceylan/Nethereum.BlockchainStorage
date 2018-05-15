using Nethereum;
using OpsICO.Core.Data;
using OpsICO.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.BlockChainStore.Data.Processors
{
  public class StorageProcessor
  {
    private readonly IUnitOfWork repositoryBase;
    private BlockProcessor blockProcessor;
    private TransactionsProcessor transactionsProcessor;
    private Web3.Web3 web3;

    public StorageProcessor(string url, IUnitOfWork repositoryBase)
    {
      web3 = new Web3.Web3(url);
      blockProcessor = new BlockProcessor(web3, repositoryBase);
      transactionsProcessor = new TransactionsProcessor(web3, repositoryBase);
      this.repositoryBase = repositoryBase;
    }

    public async Task<bool> ExecuteAsync()
    {

      var startBlock = repositoryBase.GetRepository<NodeBlock>().GetAll().Max(x => x.BlockNumber);
      var latestblock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
      new Helpers().AddLog(LogType.Info, $"DBdeki Son Blok : {startBlock} , Ağdaki Son Blok : {latestblock.Value}");
      var endBlock = (int)latestblock.Value;

      while (startBlock <= endBlock)
      {
        try
        {
          new Helpers().AddLog(LogType.Process, $"Blok-{startBlock} İşleme Alınıyor..");
          var block = await blockProcessor.ProcessBlockAsync(startBlock);
          new Helpers().AddLog(LogType.Success, $"Blok-{startBlock} İşleme Alındı..");

          new Helpers().AddLog(LogType.Info, $"Blok-{startBlock} Transactionlar İşleme Alınıyor, Tx Count : {block.TransactionHashes.Length} ..");
          await transactionsProcessor.ProcessTransactionAsync(block);
          new Helpers().AddLog(LogType.Success, $"Blok-{startBlock} Transactionlar İşleme Alındı...");
        }
        catch (Exception e)
        {
        }
        startBlock++;
      }
      return true;

    }
  }
}
