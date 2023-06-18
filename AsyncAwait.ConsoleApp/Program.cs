using AsyncAwait.ConsoleApp;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
class Program
{
    // Asynchronous

    static async Task Main(string[] args)
    {
        var clinet = new HttpClient();
        Console.WriteLine("Geting data!");
        var URL = clinet.GetStringAsync("https://dog.ceo/api/breeds/image/random");
        Console.WriteLine("Doing other task");
        var data = await URL;
        Console.WriteLine("Data Returned");



        //
        synchronous _synchronous = new synchronous();

        var breakfast = await MakeBreakfastAsync();
        Console.WriteLine("Eat breakfase ( " + breakfast + ") ");

        //
    }

    //
    public static async Task<string> MakeBreakfastAsync()
    {
        var makingCoffe = MakingCoffeAsync();

        Console.WriteLine("Heat the pan");
        Console.WriteLine("Make Omelette");

        var coffe = await makingCoffe;

        return coffe + " & omelette";
    }

    public static async Task<string> MakingCoffeAsync()
    {
        Console.WriteLine("Start heating water");
        Console.WriteLine("Waiting for finish heating");
        await Task.Delay(2000);

        Console.WriteLine("Water finished heating");
        Console.WriteLine("Make coffe");

        return "Coffe".ToString();
    }

    //
}
