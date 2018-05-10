using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;


namespace Nethereum.BlockchainStore.SQL
{
  public abstract class TransactionBase
  {
    public TransactionBase()
    {
    }

    public string BlockHash
    {
      get; set;
    }

    public abstract string Hash { get; set; }

    public string AddressFrom
    {
      get; set;
    }

    public long TimeStamp
    {
      get; set;
    }

    public long TransactionIndex
    {
      get; set;
    }

    public string Value
    {
      get; set;
    }

    public abstract string AddressTo { get; set; }
    public abstract string BlockNumber { get; set; }

    public long Gas
    {
      get; set;
    }

    public long GasPrice
    {
      get; set;
    }

    public string Input
    {
      get; set;
    }

    public long Nonce
    {
      get; set;
    }

    public bool Failed
    {
      get; set;
    }

    public string ReceiptHash
    {
      get; set;
    }

    public long GasUsed
    {
      get; set;
    }

    public long CumulativeGasUsed
    {
      get; set;
    }

    public bool HasLog
    {
      get; set;
    }

    public string Error
    {
      get; set;
    }

    public bool HasVmStack
    {
      get; set;
    }

    public string NewContractAddress
    {
      get; set;
    }

    public bool FailedCreateContract
    {
      get; set;
    }

    public static TransactionBase CreateTransaction(
        TransactionBase transaction,
        RPC.Eth.DTOs.Transaction transactionSource,
        TransactionReceipt transactionReceipt,
        bool failed,
        HexBigInteger timeStamp,
        string error = null,
        bool hasVmStack = false,
        string newContractAddress = null)
    {
      transaction.BlockHash = transactionSource.BlockHash;
      transaction.Hash = transactionSource.TransactionHash;
      transaction.AddressFrom = transactionSource.From;
      transaction.TransactionIndex = (long)transactionReceipt.TransactionIndex.Value;
      transaction.SetValue(transactionSource.Value);
      transaction.AddressTo = transactionSource.To ?? string.Empty;
      transaction.NewContractAddress = newContractAddress ?? string.Empty;
      transaction.SetBlockNumber(transactionSource.BlockNumber);
      transaction.SetGas(transactionSource.Gas);
      transaction.SetGasPrice(transactionSource.GasPrice);
      transaction.Input = transactionSource.Input ?? string.Empty;
      transaction.Nonce = (long)transactionSource.Nonce.Value;
      transaction.Failed = failed;
      transaction.SetGasUsed(transactionReceipt.GasUsed);
      transaction.SetCumulativeGasUsed(transactionReceipt.CumulativeGasUsed);
      transaction.HasLog = transactionReceipt.Logs.Count > 0;
      transaction.SetTimeStamp(timeStamp);
      transaction.Error = error ?? string.Empty;
      transaction.HasVmStack = hasVmStack;

      return transaction;
    }

    public void SetBlockNumber(HexBigInteger blockNumber)
    {
      BlockNumber = blockNumber.Value.ToString();
    }

    public void SetTimeStamp(HexBigInteger timeStamp)
    {
      TimeStamp = (long)timeStamp.Value;
    }

    public void SetGas(HexBigInteger gas)
    {
      Gas = (long)gas.Value;
    }

    public void SetGasUsed(HexBigInteger gasUsed)
    {
      GasUsed = (long)gasUsed.Value;
    }

    public void SetCumulativeGasUsed(HexBigInteger cumulativeGasUsed)
    {
      CumulativeGasUsed = (long)cumulativeGasUsed.Value;
    }

    public void SetGasPrice(HexBigInteger gasPrice)
    {
      GasPrice = (long)gasPrice.Value;
    }

    public void SetValue(HexBigInteger value)
    {
      Value = value.Value.ToString();
    }

    public BigInteger GetBlockNumber()
    {
      if (string.IsNullOrEmpty(BlockNumber)) return new BigInteger();
      return BigInteger.Parse(BlockNumber);
    }

    public BigInteger GetValue()
    {
      if (string.IsNullOrEmpty(Value)) return new BigInteger();
      return BigInteger.Parse(BlockNumber);
    }
  }
}