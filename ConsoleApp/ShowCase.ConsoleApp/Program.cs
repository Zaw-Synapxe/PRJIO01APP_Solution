using UtilityLibraries;

class Program
{
    static void Main(string[] args)
    {
        int row = 0;

        do
        {
            if (row == 0 || row >= 9)
                ResetConsole();

            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) break;
            Console.WriteLine($"Input: {input}");
            Console.WriteLine("Begins with uppercase? " +
                 $"{(input.StartsWithUpper() ? "Yes" : "No")}");
            Console.WriteLine();
            row += 4;
        } while (true);

        // ---------------------------------------
        // Read first number   
        Console.Write("Enter Number 1: ");
        var var1 = Console.ReadLine();
        // Convert string to int  
        int num1 = Convert.ToInt32(var1);
        // Read second number  
        Console.Write("Enter Number 2: ");
        var var2 = Console.ReadLine();
        // Convert string to int  
        int num2 = Convert.ToInt32(var2);
        // Read operation type - operator  
        Console.Write("Enter one Operator (Add/Sub/Mul/Div): ");
        var op = Console.ReadLine();

        // Create a class library object and call the Calculate method  
        MathClass math = new MathClass();
        double result = math.Calculate(num1, num2, op);

        Console.WriteLine("Result: {0}", result);
        // ---------------------------------------

        return;

        // Declare a ResetConsole local method
        void ResetConsole()
        {
            if (row > 0)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}Press <Enter> only to exit; otherwise, enter a string and press <Enter>:{Environment.NewLine}");
            row = 3;
        }


        //

        
    }
}
