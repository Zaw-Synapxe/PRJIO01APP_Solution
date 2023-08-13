using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.ConsoleApp
{
    public class Worker : IWorker
    {
        public void DoSomeWork()
        {
            Console.WriteLine("Do Some Work ...");
        }
    }
}
