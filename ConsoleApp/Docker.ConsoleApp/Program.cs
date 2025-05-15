namespace Docker.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World !");

            var counter = 0;
            var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
            while (max is -1 || counter < max)
            {
                Console.WriteLine($"Counter: {++counter}");
                await Task.Delay(TimeSpan.FromMilliseconds(1_000));
            }

            //
            Console.WriteLine("In C#, To use async method");
            Console.WriteLine($"Main Method execution started at {System.DateTime.Now}");
            await Task.Delay(2000);
            Console.WriteLine($"Main Method execution ended at {System.DateTime.Now}");
            Console.WriteLine("Press any key to exist.");
            Console.ReadKey();



            //
        }
    }
}


//class Program
//{
//    public static async Task<int> Main(string[] args)
//    {
//        Console.Title = "async Task<int> Main";
//        int number1 = 5, number2 = 10;
//        Console.WriteLine($"Sum of {number1} and {number2} is: {await AdditionAsync(number1, number2)}");
//        Console.WriteLine("Press any key to exist.");
//        Console.ReadKey();
//        return 0;
//    }
//    private static Task<int> AdditionAsync(int no1, int no2)
//    {
//        return Task.Run(() => SUM(no1, no2));
//        //Local function 
//        int SUM(int x, int y)
//        {
//            return x + y;
//        }
//    }
//}
