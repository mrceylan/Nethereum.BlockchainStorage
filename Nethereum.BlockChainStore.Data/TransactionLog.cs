using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.BlockChainStore.Data
{
  public class TransactionLog
  {
    public string address { get; set; }
    public string[] topics { get; set; }
    public string data { get; set; }
    public string blockNumber { get; set; }
    public string transactionHash { get; set; }
    public string transactionIndex { get; set; }
    public string blockHash { get; set; }
    public string logIndex { get; set; }
    public bool removed { get; set; }
  }
}
