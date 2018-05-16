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
      var blocks = repositoryBase.GetRepository<NodeBlock>().GetAll();
      int startBlock = 5623328;
      if (blocks.Count() > 0)
        startBlock = blocks.Max(x => x.BlockNumber);
      var latestblock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
      new Helpers().AddLog(LogType.Info, $"Last Block in DB : {startBlock} , Last Block in Network : {latestblock.Value}");
      var endBlock = (int)latestblock.Value;

      while (startBlock <= endBlock)
      {
        try
        {
          new Helpers().AddLog(LogType.Process, $"Block-{startBlock} Processing");
          var block = await blockProcessor.ProcessBlockAsync(startBlock);
          new Helpers().AddLog(LogType.Success, $"Block-{startBlock} Processed");

          new Helpers().AddLog(LogType.Info, $"Block-{startBlock} Transactions Processing, Tx Count : {block.TransactionHashes.Length} ..");
          await transactionsProcessor.ProcessTransactionAsync(block);
          new Helpers().AddLog(LogType.Success, $"Block-{startBlock} Transactions Processed");
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
