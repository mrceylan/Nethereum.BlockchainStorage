using OpsICO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Entities
{
    public class NodeTransaction
    {
        public int NodeTransactionID { get; set; }
        public string WalletAddress { get; set; }
        public string ContractAddress { get; set; }
        public DateTime TxTime { get; set; }
        public TransactionStatus Status { get; set; }
        public int BlockOrderNo { get; set; }
        public int BlockNumber { get; set; }
        public string Hash { get; set; }


        public virtual ICollection<NodeTransactionDetail> NodeTransactionDetails { get; set; }


  }
}
