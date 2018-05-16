using Microsoft.EntityFrameworkCore;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum;
using OpsICO;
using OpsICO.Core.Data;
using OpsICO.Core.Entities;
using OpsICO.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI;

namespace Nethereum.BlockChainStore.Data.Processors
{
  public class TransactionsProcessor
  {
    private readonly IRepository<NodeTransaction> transactionRepository;
    private readonly IRepository<NodeBlock> blockRepository;
    private Web3.Web3 web3;
    private IUnitOfWork repositoryBase;
    private NodeBlock nodeBlock;
    private string TransferEventKeccak = "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef"; //keccak of transfer event

    public TransactionsProcessor(Web3.Web3 web3, IUnitOfWork _repositoryBase)
    {
      this.web3 = web3;
      this.repositoryBase = _repositoryBase;
      transactionRepository = _repositoryBase.GetRepository<NodeTransaction>();
      blockRepository = _repositoryBase.GetRepository<NodeBlock>();
    }

    public virtual async Task ProcessTransactionAsync(BlockWithTransactionHashes block)
    {
      nodeBlock = blockRepository.Get(x => x.BlockNumber == block.Number.Value).FirstOrDefault();

      if (nodeBlock?.NodeTransactions != null) 
        return;

      nodeBlock.NodeTransactions = new List<NodeTransaction>();

      foreach (var _hash in block.TransactionHashes)
      {
        await CheckandFillTransactionAsync(_hash, block.Timestamp.Value);
      }
      try
      {
        repositoryBase.Commit();
      }
      catch (Exception e)
      {
        new Helpers().AddLog(LogType.Failure, $"Block-{block.Number.Value} Transactions DB Write Error => " + e.InnerException);
      }
    }

    private async Task CheckandFillTransactionAsync(string hash, BigInteger blocktime)
    {
      var transactionSource = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(hash);

      var transactionReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash).ConfigureAwait(false);

      new Helpers().AddLog(LogType.Process, $"Block-{transactionSource.BlockNumber.Value} Tx-{hash} Found, Processing");
      var transaction = new NodeTransaction();
      try
      {

        transaction = new NodeTransaction
        {
          NodeTokenTransfers = new List<NodeTokenTransfer>(),
          From = transactionSource.From,
          To = transactionSource.To,
          Status = (OpsICO.Core.Enums.TransactionStatus)((int)transactionReceipt.Status.Value),
          TxHash = transactionSource.TransactionHash,
          Value = new UnitConversion().FromWei(transactionSource.Value.Value)
        };

      }
      catch (Exception e)
      {
        new Helpers().AddLog(LogType.Failure, $"Block-{transactionSource.BlockNumber.Value} Tx-{hash} Entity Create Error => " + e.InnerException);
      }

      new Helpers().AddLog(LogType.Process, $"Block-{transactionSource.BlockNumber.Value} Tx-{hash} Logs Processing");
      ProcessTransactionLogs(transactionSource, transactionReceipt, blocktime, ref transaction);
      new Helpers().AddLog(LogType.Success, $"Block-{transactionSource.BlockNumber.Value} Tx-{hash} Logs Processed");

      nodeBlock.NodeTransactions.Add(transaction);
    }

    private void ProcessTransactionLogs(Transaction transactionSource, TransactionReceipt transactionReceipt, BigInteger blocktime, ref NodeTransaction transaction)
    {
      if (!transactionReceipt.Logs.HasValues)
        return;

      int _order = 1;
      var _txlogs = transactionReceipt.Logs.ToObject<List<TransactionLog>>();
      foreach (var _log in _txlogs)
      {
        try
        {
          if (_log.topics.First() != TransferEventKeccak)
            continue;
          if (_log.topics.Length < 3)
            continue;

          transaction.NodeTokenTransfers.Add(new NodeTokenTransfer()
          {
            Amount = new Helpers().HextoString(_log.data),
            From = new AddressType().Decode<string>(_log.topics[1]),
            To = new AddressType().Decode<string>(_log.topics[2]),
            TokenContractAddress = transactionSource.To
          });
          _order++;
        }
        catch (Exception e)
        {
          new Helpers().AddLog(LogType.Failure, $"Block-{transactionReceipt.BlockNumber.Value} Tx-{transactionReceipt.TransactionHash} Log Entity Create Error => " + e.InnerException);
        }
      }
    }


  }
}
