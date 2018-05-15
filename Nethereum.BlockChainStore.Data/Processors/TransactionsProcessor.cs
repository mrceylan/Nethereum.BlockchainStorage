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

namespace Nethereum.BlockChainStore.Data.Processors
{
  public class TransactionsProcessor
  {
    private readonly IRepository<NodeTransaction> transactionRepository;
    private Web3.Web3 web3;
    private IUnitOfWork repositoryBase;
    private NodeTransaction transaction;
    private string TransferEventKeccak = "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef";

    public TransactionsProcessor(Web3.Web3 web3, IUnitOfWork _repositoryBase)
    {
      this.web3 = web3;
      this.repositoryBase = _repositoryBase;
      transactionRepository = _repositoryBase.GetRepository<NodeTransaction>();
    }

    public virtual async Task ProcessTransactionAsync(BlockWithTransactionHashes block)
    {

      var txs = transactionRepository.Get(x => x.BlockNumber == (int)block.Number.Value).FirstOrDefault();

      if (txs != null) // eğer bu bloka ait transaction varsa içerde atlıyoruz
        return;

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
        new Helpers().AddLog(LogType.Failure, $"Blok-{block.Number.Value} Transactions DB Yazma Hatası => " + e.InnerException);
      }
    }

    private async Task CheckandFillTransactionAsync(string hash, BigInteger blocktime)
    {
      var transactionSource = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(hash);

      if (string.IsNullOrEmpty(transactionSource.To))
        return;
      if (transactionSource.Value.Value <= 0)
        return;

      var transactionReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash).ConfigureAwait(false);

      transaction = new NodeTransaction
      {
        NodeTransactionDetails = new List<NodeTransactionDetail>()
      };

      new Helpers().AddLog(LogType.Process, $"Blok-{transactionSource.BlockNumber.Value} Tx-{hash} Bulundu, İşleme Alınıyor");

      try
      {
        transaction.BlockNumber = (int)transactionSource.BlockNumber.Value;
        transaction.BlockOrderNo = (int)transactionReceipt.TransactionIndex.Value;
        transaction.ContractAddress = transactionSource.To;
        transaction.Hash = transactionSource.TransactionHash;
        transaction.Status = (OpsICO.Core.Enums.TransactionStatus)((int)transactionReceipt.Status.Value);
        transaction.TxTime = new Helpers().UnixTimeStampToDateTime((double)blocktime);
        transaction.WalletAddress = transactionSource.From;

        transaction.NodeTransactionDetails.Add(new NodeTransactionDetail()
        {
          Amount = new UnitConversion().FromWei(transactionSource.Value.Value),
          OrderNo = 0,
          TransactionDirection = OpsICO.Core.Enums.TransactionDirection.ToContract
        });

      }
      catch (Exception e)
      {
        new Helpers().AddLog(LogType.Failure, $"Blok-{transactionSource.BlockNumber.Value} Tx-{hash} Entity Yazma Hatası => " + e.InnerException);
      }

      new Helpers().AddLog(LogType.Process, $"Blok-{transactionSource.BlockNumber.Value} Tx-{hash} Loglar İşleme Alınıyor..");
      ProcessTransactionLogs(transactionReceipt, blocktime);
      new Helpers().AddLog(LogType.Success, $"Blok-{transactionSource.BlockNumber.Value} Tx-{hash} Loglar İşleme Alındı.");

      transactionRepository.Add(transaction);
    }

    private void ProcessTransactionLogs(TransactionReceipt transactionReceipt, BigInteger blocktime)
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

          transaction.NodeTransactionDetails.Add(new NodeTransactionDetail()
          {
            Amount = new Helpers().HextoDecimal(_log.data, 18), //emre buraya bak
            OrderNo = _order,
            TransactionDirection = OpsICO.Core.Enums.TransactionDirection.FromContract
          });
          _order++;
        }
        catch (Exception e)
        {
          new Helpers().AddLog(LogType.Failure, $"Blok-{transactionReceipt.BlockNumber.Value} Tx-{transactionReceipt.TransactionHash} Log Yazma Hatası => " + e.InnerException);
        }
      }
    }


  }
}
