using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.ConsoleApp
{
    class synchronous
    {
        static void Main(string[] args)
        {
            var breakfast = MakeBreakfast();
            Console.WriteLine("Eat breakfast ( " + breakfast + " )");
        }

        public static string MakeBreakfast()
        {
            var coffe = MakeCoffe();
            Console.WriteLine("Heat the pan");
            Console.WriteLine("Make Omelette");

            return coffe + " & omelette";
        }

        public static string MakeCoffe()
        {
            Console.WriteLine("Start heating water");
            Console.WriteLine("Waiting for finish heating");

            Task.Delay(2000).GetAwaiter().GetResult();

            Console.WriteLine("Water finished heating");
            Console.WriteLine("Make coffe");

            return "Coffe".ToString();
        }

        //
    }
}
