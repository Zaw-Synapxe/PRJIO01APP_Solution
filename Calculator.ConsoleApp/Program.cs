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
                    await Task.Delay(TimeSpan.FromSeconds(0.5));
                    return Convert.ToDouble(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid number. Please try again: ");
                }
            }
        }

        // -----------------------

        // Check if a number is prime and return true if it is, false otherwise
        private static bool BadCheckIfPrime(int number)
        {
            //////// Bad Method
            //////// This method includes unnecessary and complex code
            //////// It is difficult to read and understand
            //////// It is also inefficient
            //////for (int i = 2; i < number; i++)
            //////{
            //////    if (number % i == 0)
            //////    {
            //////        return false;
            //////    }
            //////}
            //////return true;

            // Good Method
            // This method is short and concise
            // It only includes the code necessary to achieve its purpose
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool GoodCheckIfPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2 || number == 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;

            int limit = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 5; i <= limit; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0) return false;
            }

            return true;
        }

        // Calculate the average temperature for a list of cities
        public double CalculateAverageTemperature(List<City> cities)
        {
            // Validate input parameters
            if (cities == null || cities.Count == 0)
            {
                throw new ArgumentException("Input list cannot be null or empty");
            }

            // Calculate the total temperature and count of cities
            double totalTemperature = 0;
            int cityCount = 0;
            foreach (City city in cities)
            {
                // Skip cities with missing temperature data
                ////if (city.Temperature == null) continue;

                // Add temperature to total and increment city count
                totalTemperature += (double)city.Temperature;
                cityCount++;
            }

            // Calculate the average temperature and return
            if (cityCount > 0)
            {
                return totalTemperature / cityCount;
            }
            else
            {
                throw new ArgumentException("No cities with temperature data were found in the input list");
            }
        }

        //
    }

    public class City
    {
        public int cityId { get; set; }
        public string cityName { get; set; }
        public long Temperature { get; set; }
    }
}