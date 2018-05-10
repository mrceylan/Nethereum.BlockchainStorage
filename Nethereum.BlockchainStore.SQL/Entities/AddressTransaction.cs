
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System.ComponentModel.DataAnnotations;


namespace Nethereum.BlockchainStore.SQL
{
  public class AddressTransaction : TransactionBase
  {
    public AddressTransaction()
    {
    }

    [Key]
    public override string Hash
    {
      get; set;
    }

    public string Address
    {
      get; set;
    }

    public override string AddressTo
    {
      get; set;
    }

    //Store as a string so it can be parsed as a BigInteger
    public override string BlockNumber
    {
      get; set;
    }

    public static AddressTransaction CreateAddressTransaction(RPC.Eth.DTOs.Transaction transactionSource,
        TransactionReceipt transactionReceipt,
        bool failed,
        HexBigInteger timeStamp,
        string address,
        string error = null,
        bool hasVmStack = false,
        string newContractAddress = null
    )
    {
      var transaction = new AddressTransaction() { Address = address ?? string.Empty };
      //transaction.SetRowKey(transactionSource.BlockNumber, transactionSource.TransactionHash);
      return
          (AddressTransaction)
          CreateTransaction(transaction, transactionSource, transactionReceipt, failed, timeStamp, error,
              hasVmStack, newContractAddress);
    }

    //public void SetRowKey(HexBigInteger blockNumber, string transactionHash)
    //{
    //  RowKey = blockNumber.Value.ToString().ToLowerInvariant().HtmlEncode() + "_" +
    //           transactionHash.ToLowerInvariant().HtmlEncode();
    //}
  }
}