using OpsICO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Entities
{
  public class NodeTransaction
  {
    public int NodeTransactionID { get; set; }
    public int NodeBlockID { get; set; }
    public string TxHash { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Value { get; set; }
    public TransactionStatus Status { get; set; }

    public virtual ICollection<NodeTokenTransfer> NodeTokenTransfers { get; set; }
    public virtual NodeBlock NodeBlock { get; set; }

  }
}
