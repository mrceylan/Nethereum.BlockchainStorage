using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Nethereum.Hex.HexTypes;

namespace Nethereum.BlockchainStore.SQL
{
  public class Block
  {
    public Block()
    {
    }

    [Key]
    public string BlockNumber
    {
      get; set;
    }

    public string Hash
    {
      get; set;
    }

    public string ParentHash
    {
      get; set;
    }

    public string Nonce
    {
      get; set;
    }

    public string ExtraData
    {
      get; set;
    }

    public long Difficulty
    {
      get; set;
    }

    public long TotalDifficulty
    {
      get; set;
    }

    public long Size
    {
      get; set;
    }

    public string Miner
    {
      get; set;
    }

    public long GasLimit
    {
      get; set;
    }

    public long GasUsed
    {
      get; set;
    }

    public long TimeStamp
    {
      get; set;
    }

    public long TransactionCount
    {
      get; set;
    }

    public void SetBlockNumber(HexBigInteger blockNumber)
    {
      BlockNumber = blockNumber.Value.ToString();
    }

    public void SetDifficulty(HexBigInteger difficulty)
    {
      Difficulty = (long)difficulty.Value;
    }

    public void SetTotalDifficulty(HexBigInteger totalDifficulty)
    {
      TotalDifficulty = (long)totalDifficulty.Value;
    }

    public void SetTimeStamp(HexBigInteger timeStamp)
    {
      TimeStamp = (long)timeStamp.Value;
    }

    public void SetGasUsed(HexBigInteger gasUsed)
    {
      GasUsed = (long)gasUsed.Value;
    }

    public void SetGasLimit(HexBigInteger gasLimit)
    {
      GasLimit = (long)gasLimit.Value;
    }

    public void SetSize(HexBigInteger size)
    {
      Size = (long)size.Value;
    }

    public BigInteger GetBlockNumber()
    {
      var flag = string.IsNullOrEmpty(BlockNumber);
      BigInteger result;
      if (flag)
        result = default(BigInteger);
      else
        result = BigInteger.Parse(BlockNumber);
      return result;
    }
  }
}