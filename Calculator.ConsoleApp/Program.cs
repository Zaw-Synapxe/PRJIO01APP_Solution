using System;

namespace Calculator.ConloseApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter the first number: ");
                double? num1 = await ReadNumberAsync();
                if (!num1.HasValue)
                {
                    Console.WriteLine("First number cannot be null");
                    continue;
                }

                Console.WriteLine("Enter the second number: ");
                double? num2 = await ReadNumberAsync();
                if (!num2.HasValue)
                {
                    Console.WriteLine("Second number cannot be null");
                    continue;
                }

                Console.WriteLine("Enter the operator (+, -, *, /, %): ");
                string op = Console.ReadLine();

                double result;
                switch (op)
                {
                    case "+":
                        result = num1.Value + num2.Value;
                        break;
                    case "-":
                        result = num1.Value - num2.Value;
                        break;
                    case "*":
                        result = num1.Value * num2.Value;
                        break;
                    case "/":
                        if (num2 == 0)
                        {
                            Console.WriteLine("Cannot divide by zero");
                            continue;
                        }
                        result = num1.Value / num2.Value;
                        break;
                    case "%":
                        result = num1.Value % num2.Value;
                        break;
                    default:
                        Console.WriteLine("Invalid operator");
                        continue;
                }

                Console.WriteLine("Result: " + result);

                Console.WriteLine("Do you want to continue (y/n)? ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    break;
                }
            }
        }

        static async System.Threading.Tasks.Task<double?> ReadNumberAsync()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return null;
                }

                try
                {
                    return Convert.ToDouble(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid number. Please try again: ");
                }
            }
        }
    }
}