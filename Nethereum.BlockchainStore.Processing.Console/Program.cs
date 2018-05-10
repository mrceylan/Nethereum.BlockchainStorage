using System;
using System.Diagnostics;

namespace Nethereum.BlockchainStore.Processing.Console
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      
      var url = System.Configuration.ConfigurationManager.AppSettings["web3url"];
      var start = Convert.ToInt32(args[1]);
      var end = Convert.ToInt32(args[2]);

      var proc = new StorageProcessor(url);
      proc.Init().Wait();
      var result = proc.ExecuteAsync(start, end).Result;

      Debug.WriteLine(result);
      System.Console.WriteLine(result);
      System.Console.ReadLine();
    }
  }
}