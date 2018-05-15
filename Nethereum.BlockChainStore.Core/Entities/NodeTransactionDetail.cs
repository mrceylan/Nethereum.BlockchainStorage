using OpsICO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Entities
{
    public class NodeTransactionDetail
    {
        public int NodeTransactionDetailId { get; set; }
        public decimal Amount { get; set; }
        public int TokenContractID { get; set; }
        public int OrderNo { get; set; }
        public TransactionDirection TransactionDirection { get; set; }

        public int NodeTransactionId { get; set; }
        public virtual NodeTransaction NodeTransaction { get; set; }
    }
}
