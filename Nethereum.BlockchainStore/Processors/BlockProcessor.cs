using System.Diagnostics;
using System.Threading.Tasks;
using Nethereum.BlockchainStore.Processors;
using Nethereum.BlockchainStore.Processors.Transactions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.BlockchainStore.Repositories
{
  public class BlockProcessor : IBlockProcessor
  {
    private readonly IBlockRepository _blockRepository;
    private readonly ITransactionRepository _transactionRepository;

    public BlockProcessor(Web3.Web3 web3,
        IBlockRepository blockRepository,
        ITransactionProcessor transactionProcessor,
        ITransactionRepository transactionRepository
       )
    {
      _blockRepository = blockRepository;
      _transactionRepository = transactionRepository;
      TransactionProcessor = transactionProcessor;
      Web3 = web3;
    }

    protected Web3.Web3 Web3 { get; set; }
    protected ITransactionProcessor TransactionProcessor { get; set; }

    public virtual async Task ProcessBlockAsync(long blockNumber)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      var block = await GetBlockWithTransactionHashesAsync(blockNumber);
      stopwatch.Stop();
      System.Console.WriteLine("Blok bilgisi alma : " + stopwatch.Elapsed.TotalSeconds);

      stopwatch.Reset();
      stopwatch.Start();
      await _blockRepository.UpsertBlockAsync(block);
      stopwatch.Stop();
      System.Console.WriteLine("Blok bilgisi VT ye yazma : " + stopwatch.Elapsed.TotalSeconds);


      await ProcessTransactions(block);
    }

    protected async Task ProcessTransactions(BlockWithTransactionHashes block)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      foreach (var txnHash in block.TransactionHashes)
      {
        await TransactionProcessor.ProcessTransactionAsync(txnHash, block).ConfigureAwait(false);
      }
      stopwatch.Stop();
      System.Console.WriteLine("Total Transaction : " + block.TransactionHashes.Length);
      System.Console.WriteLine("Blok içindeki transaction bilgilerini alma : " + stopwatch.Elapsed.TotalSeconds);


      await _transactionRepository.AddTransactions();
    }

    protected async Task<BlockWithTransactionHashes> GetBlockWithTransactionHashesAsync(long blockNumber)
    {
      var block =
          await
              Web3.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(
                  new HexBigInteger(blockNumber)).ConfigureAwait(false);
      return block;
    }
  }
}