
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System.ComponentModel.DataAnnotations;


namespace Nethereum.BlockchainStore.SQL
{
  public class Transaction : TransactionBase
  {
    public Transaction()
    {
    }

    [Key]
    public override string Hash
    {
      get; set;
    }


    public override string BlockNumber
    {
      get; set;
    }

    public override string AddressTo
    {
      get; set;
    }

    public static Transaction CreateTransaction(RPC.Eth.DTOs.Transaction transactionSource,
        TransactionReceipt transactionReceipt,
        bool failed,
        HexBigInteger timeStamp, bool hasVmStack = false, string error = null)
    {
      return
          (Transaction)
          CreateTransaction(new Transaction(), transactionSource, transactionReceipt,
              failed, timeStamp, error, hasVmStack);
    }


    public static Transaction CreateTransaction(RPC.Eth.DTOs.Transaction transactionSource,
        TransactionReceipt transactionReceipt,
        bool failed,
        HexBigInteger timeStamp, string newContractAddress)
    {
      return
          (Transaction)
          CreateTransaction(new Transaction(), transactionSource, transactionReceipt,
              failed, timeStamp, null, false, newContractAddress);
    }
  }
}