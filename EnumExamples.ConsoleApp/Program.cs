
namespace EnumExamples.ConsoleApp
{
    [Flags]
    public enum Month
    {
        Jan = 1,
        Feb = 2,
        Mar = 4,
        Apr = 8,
        May = 16,
        Jun = 32,
        Jul = 64,
        Aug = 128,
        Sep = 256,
        Oct = 512,
        Nov = 1024,
        Dec = 2048
    }

    public enum ReportType
    {
        Sum,
        Average,
        Min,
        Max
    }

    class Program
    {
        public static void ProcessMonthlyExpenditureData(Month month)
        {
            switch (month)
            {
                case Month.Jan:
                    Console.WriteLine("Processing data for Jan...");
                    Console.WriteLine($"The value of month is {(int)month}");
                    break;
                case Month.Feb:
                    Console.WriteLine("Processing data for Feb...");
                    break;
                case Month.Mar:
                    Console.WriteLine("Processing data for Mar...");
                    break;
                case Month.Apr:
                    Console.WriteLine("Processing data for Apr...");
                    break;
                case Month.May:
                    Console.WriteLine("Processing data for May...");
                    break;
                case Month.Jun:
                    Console.WriteLine("Processing data for Jun...");
                    break;
                case Month.Jul:
                    Console.WriteLine("Processing data for Jul...");
                    break;
                case Month.Aug:
                    Console.WriteLine("Processing data for Aug...");
                    Console.WriteLine($"The value of month is {(int)month}");
                    break;
                case Month.Sep:
                    Console.WriteLine("Processing data for Sep...");
                    break;
                case Month.Oct:
                    Console.WriteLine("Processing data for Oct...");
                    break;
                case Month.Nov:
                    Console.WriteLine("Processing data for Nov...");
                    break;
                case Month.Dec:
                    Console.WriteLine("Processing data for Dec...");
                    break;
                default:
                    throw new Exception("Invalid Month");
            }

        }

        static void Main(string[] args)
        {
            decimal[] data = new decimal[12];

            PopulateMonthlyExpenditureData(data);

            Month months = Month.Apr | Month.Aug;

            decimal[] reportData = GetReportData(months, data);

            OutputReport(ReportType.Sum, months, reportData);
            OutputReport(ReportType.Average, months, reportData);
            OutputReport(ReportType.Min, months, reportData);
            OutputReport(ReportType.Max, months, reportData);

            Console.ReadKey();
        }

        public static void OutputReport(ReportType reportType, Month includedMonths, decimal[] reportData)
        {
            switch (reportType)
            {
                case ReportType.Sum:
                    Console.WriteLine($"Total expenditure for months, {includedMonths} is {reportData.Sum()}");
                    break;
                case ReportType.Average:
                    Console.WriteLine($"Average expenditure for months, {includedMonths} is {reportData.Average()}");
                    break;
                case ReportType.Min:
                    Console.WriteLine($"Min expenditure for months, {includedMonths} is {reportData.Min()}");
                    break;
                case ReportType.Max:
                    Console.WriteLine($"Max expenditure for months, {includedMonths} is {reportData.Max()}");
                    break;
                default:
                    throw new Exception("Invalid report type!");
            }

        }

        public static decimal[] GetReportData(Month months, decimal[] data)
        {
            int count = 0;
            int testMonthInclusion = 0;

            int reportDataLength = CountBits((int)months);

            decimal[] reportData = new decimal[reportDataLength];

            int index = 0;

            foreach (var item in Enum.GetValues(typeof(Month)))
            {
                testMonthInclusion = (int)months & (int)item;

                if (testMonthInclusion > 0)
                {
                    index = (int)Math.Round(Math.Log((int)item, 2));

                    reportData[count] = data[index];

                    count++;
                }

            }

            return reportData;
        }


        public static int CountBits(int value)
        {
            //Brian Kernighan's Algorithm
            //Counts the number of set bits

            int count = 0;

            while (value != 0)
            {
                count++;
                value &= value - 1;
            }

            return count;

        }

        public static void PopulateMonthlyExpenditureData(decimal[] data)
        {
            data[0] = 5000;
            data[1] = 3000.50m;
            data[2] = 4000.3m;
            data[3] = 2000;
            data[4] = 3500;
            data[5] = 4000.2m;
            data[6] = 1000;
            data[7] = 500;
            data[8] = 600;
            data[9] = 6000;
            data[10] = 3000;
            data[11] = 10000;

        }

    }
}