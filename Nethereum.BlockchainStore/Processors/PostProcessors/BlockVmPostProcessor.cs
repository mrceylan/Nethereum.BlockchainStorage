using System.Threading.Tasks;
using Nethereum.BlockchainStore.Processors.Transactions;
using Nethereum.BlockchainStore.Repositories;

namespace Nethereum.BlockchainStore.Processors.PostProcessors
{
    public class BlockVmPostProcessor : BlockPostProcessor
    {
        public BlockVmPostProcessor(Web3.Web3 web3, IBlockRepository blockRepository, ITransactionProcessor transactionProcessor, ITransactionRepository transactionRepository) : base(web3, blockRepository, transactionProcessor, transactionRepository)
        {
        }

        public override async Task ProcessBlockAsync(long blockNumber)
        {
            TransactionProcessor.EnabledValueProcessing = false;
            TransactionProcessor.EnabledContractCreationProcessing = false;
            TransactionProcessor.EnabledContractProcessing = true;
            TransactionProcessor.ContractTransactionProcessor.EnabledVmProcessing = true;
            await base.ProcessBlockAsync(blockNumber);
        }
    }
}