using System;
using System.Collections.Generic;
using System.Text;

namespace OpsICO.Core.Entities
{
    public class NodeBlock
    {
        public int NodeBlockID { get; set; }
        public int BlockNumber { get; set; }
        public string Hash { get; set; }
        public string ParentHash { get; set; }
        public DateTime BlockTime { get; set; }
        public int TransactionCount { get; set; }
    }
}
