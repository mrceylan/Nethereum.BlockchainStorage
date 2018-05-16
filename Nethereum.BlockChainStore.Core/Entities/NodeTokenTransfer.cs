using OpsICO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Entities
{
  public class NodeTokenTransfer
  {
    public int NodeTokenTransferID { get; set; }
    public int NodeTransactionID { get; set; }
    public string TokenContractAddress { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Amount { get; set; }

    public virtual NodeTransaction NodeTransaction { get; set; }
  }
}
