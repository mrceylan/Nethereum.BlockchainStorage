using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum;
using OpsICO.Core.Data;
using OpsICO.Core.Entities;
using OpsICO.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.BlockChainStore.Data.Processors
{
  public class BlockProcessor
  {
    private readonly Web3.Web3 web3;
    private readonly IRepository<NodeBlock> blockRepository;
    private IUnitOfWork repositoryBase;

    public BlockProcessor(Web3.Web3 web3, IUnitOfWork _repositoryBase)
    {
      this.web3 = web3;
      this.repositoryBase = _repositoryBase;
      blockRepository = _repositoryBase.GetRepository<NodeBlock>();
    }

    public virtual async Task<BlockWithTransactionHashes> ProcessBlockAsync(long blockNumber)
    {
      var block = await GetBlockWithTransactionHashesAsync(blockNumber);
      if (block == null)
        return null;
      InsertBlock(block);
      return block;
    }

    protected async Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesAsync(long blockNumber)
    {
      try
      {
        var block =
             await
                 web3.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(
                     new HexBigInteger(blockNumber)).ConfigureAwait(false);
        return block;
      }
      catch (Exception e)
      {
        new Helpers().AddLog(LogType.Failure, $"Blok-{blockNumber} Data Çekme Hatası => " + e.InnerException);
        return null;
      }
    }

    protected void InsertBlock(BlockWithTransactionHashes block)
    {
      try
      {

        var isblock = blockRepository.Get(x => x.BlockNumber == (int)block.Number.Value).FirstOrDefault();

        if (isblock != null) //blok içerde zaten varsa atlıyoruz
          return;

        var _nodeBlock = new NodeBlock()
        {
          BlockNumber = (int)block.Number.Value,
          BlockTime = new Helpers().UnixTimeStampToDateTime((double)block.Timestamp.Value),
          Hash = block.BlockHash,
          ParentHash = block.ParentHash,
          TransactionCount = block.TransactionHashes.Length
        };

        blockRepository.Add(_nodeBlock);
        repositoryBase.Commit();
      }
      catch (Exception e)
      {

        new Helpers().AddLog(LogType.Failure, $"Blok-{block.Number.Value} DB Yazma Hatası => " + e.InnerException);
      }
    }

  }
}
