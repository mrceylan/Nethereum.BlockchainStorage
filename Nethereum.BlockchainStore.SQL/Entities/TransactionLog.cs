
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nethereum.BlockchainStore.SQL
{
  public class TransactionLog
  {
    public TransactionLog()
    {
    }

    [Key, Column(Order = 0)]
    public string TransactionHash
    {
      get; set;
    }

    [Key, Column(Order = 1)]
    public long LogIndex
    {
      get; set;
    }

    public string Address
    {
      get; set;
    }

    public string Topics
    {
      get; set;
    }

    public string Topic0
    {
      get; set;
    }

    public string Data
    {
      get; set;
    }

    public static TransactionLog CreateTransactionLog(string transactionHash, long logIndex,
        JObject log)
    {
      var transactionLog = new TransactionLog() { TransactionHash = transactionHash, LogIndex = logIndex };
      transactionLog.InitLog(log);
      return transactionLog;
    }

    public void InitLog(JObject logObject)
    {
      Address = logObject["address"].Value<string>() ?? string.Empty;
      Data = logObject["data"].Value<string>() ?? string.Empty;
      var topics = logObject["topics"] as JArray;
      if (topics != null)
      {
        Topics = topics.ToString();
        if (topics.Count > 0)
          Topic0 = topics[0].ToString();
      }
    }
  }
}