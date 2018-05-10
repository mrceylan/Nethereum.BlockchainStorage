using System;
using System.Diagnostics;

namespace Nethereum.BlockchainStore.Processing.Console
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      //string url = "http://localhost:8045";
      //int start = 500599;
      //int end = 1000000;
      //bool postVm = true;

      var url = "https://ropsten.infura.io/2riHiBOAVSxHOkL6DfLi";
      var start = Convert.ToInt32(args[1]);
      var end = Convert.ToInt32(args[2]);
      //var postVm = false;
      //if (args.Length > 3)
      //  if (args[3].ToLower() == "postvm")
      //    postVm = true;

      var proc = new StorageProcessor(url);
      proc.Init().Wait();
      var result = proc.ExecuteAsync(start, end).Result;

      Debug.WriteLine(result);
      System.Console.WriteLine(result);
      System.Console.ReadLine();
    }
  }
}