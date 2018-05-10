using System.Data.Entity;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Processors;
using Nethereum.BlockchainStore.SQL.Context;
using Nethereum.RPC.Eth.DTOs;
using Block = Nethereum.BlockchainStore.SQL.Block;

namespace Nethereum.BlockchainStore.SQL
{
  public class BlockRepository : IBlockRepository
  {


    public BlockRepository()
    {

    }

    public async Task UpsertBlockAsync(BlockWithTransactionHashes block)
    {
      var blockEntity = MapBlock(block, new Block());
      //await blockEntity.InsertOrReplaceAsync().ConfigureAwait(false);
      await InsertOrUpdate(blockEntity);
    }

    public Block MapBlock(BlockWithTransactionHashes blockSource, Block blockOutput)
    {
      blockOutput.SetBlockNumber(blockSource.Number);
      blockOutput.SetDifficulty(blockSource.Difficulty);
      blockOutput.SetGasLimit(blockSource.GasLimit);
      blockOutput.SetGasUsed(blockSource.GasUsed);
      blockOutput.SetSize(blockSource.Size);
      blockOutput.SetTimeStamp(blockSource.Timestamp);
      blockOutput.SetTotalDifficulty(blockSource.TotalDifficulty);
      blockOutput.ExtraData = blockSource.ExtraData ?? string.Empty;
      blockOutput.Hash = blockSource.BlockHash ?? string.Empty;
      blockOutput.ParentHash = blockSource.ParentHash ?? string.Empty;
      blockOutput.Miner = blockOutput.Miner ?? string.Empty;
      blockOutput.Nonce = blockOutput.Nonce ?? string.Empty;
      blockOutput.TransactionCount = blockSource.TransactionHashes.Length;

      return blockOutput;
    }

    public async Task InsertOrUpdate(Block block)
    {
      using (var context = new BlockchainStoreContext())
      {
        //context.Entry(block).State = string.IsNullOrEmpty(block.BlockNumber) ?
        //                           EntityState.Added :
        //                           EntityState.Modified;
        try
        {
          context.Blocks.Add(block);
          await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
        }
      }
    }
  }
}