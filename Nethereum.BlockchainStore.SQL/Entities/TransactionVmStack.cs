using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Nethereum.BlockchainStore.SQL
{
  public class TransactionVmStack
  {
    public TransactionVmStack()
    {
    }

    [Key, Column(Order = 0)]
    public string Address
    {
      get; set;
    }

    [Key, Column(Order = 1)]
    public string TransactionHash
    {
      get; set;
    }

    public string StructLogs1
    {
      get; set;
    }

    public string StructLogs2
    {
      get; set;
    }

    public string StructLogs3
    {
      get; set;
    }

    public string StructLogs4
    {
      get; set;
    }

    public string StructLogs5
    {
      get; set;
    }


    public string StructLogs6
    {
      get; set;
    }

    public string StructLogs7
    {
      get; set;
    }

    public string StructLogs8
    {
      get; set;
    }

    public string StructLogs9
    {
      get; set;
    }

    public string StructLogs10
    {
      get; set;
    }

    public string StructLogs11
    {
      get; set;
    }

    public string StructLogs12
    {
      get; set;
    }

    public string StructLogs13
    {
      get; set;
    }

    public string StructLogs14
    {
      get; set;
    }

    public string StructLogs15
    {
      get; set;
    }

    public string StructLogs16
    {
      get; set;
    }

    public string StructLogs17
    {
      get; set;
    }

    public string StructLogs18
    {
      get; set;
    }

    public string StructLogs19
    {
      get; set;
    }

    public string StructLogs20
    {
      get; set;
    }

    public string StructLogs21
    {
      get; set;
    }

    public string StructLogs22
    {
      get; set;
    }

    public string StructLogs23
    {
      get; set;
    }

    public string StructLogs24
    {
      get; set;
    }

    public string StructLogs25
    {
      get; set;
    }

    public static TransactionVmStack CreateTransactionVmStack(string transactionHash,
        string address,
        JObject stack)
    {
      var structsLogs = (JArray)stack["structLogs"];
      var transactionVmStack = new TransactionVmStack()
      {
        TransactionHash = transactionHash,
        Address = address
      };
      transactionVmStack.InitStruct(structsLogs);
      return transactionVmStack;
    }

    public void InitStruct(JArray structLogs)
    {
      var currentProperty = 1;
      var maxProperty = 15;
      var logProperty = new JArray();
      foreach (var structLog in structLogs)
      {
        logProperty.Add(structLog);
        //to make sure it fits in 64k just use 28k which * 2 is 56k (utf16)
        if (Encoding.Unicode.GetByteCount(logProperty.ToString()) > 56 * 1024)
        {
          logProperty.Remove(structLog);
          var property = GetType().GetProperty("StructLogs" + currentProperty);
          property.SetValue(this, logProperty.ToString());

          currentProperty = currentProperty + 1;
          if (currentProperty <= maxProperty)
          {
            logProperty = new JArray();
            logProperty.Add(structLog);
          }
          else
          {
            return;
          }
        }
      }

      if (currentProperty <= maxProperty)
      {
        var property = GetType().GetProperty("StructLogs" + currentProperty);
        property.SetValue(this, logProperty.ToString());
      }
    }
  }
}