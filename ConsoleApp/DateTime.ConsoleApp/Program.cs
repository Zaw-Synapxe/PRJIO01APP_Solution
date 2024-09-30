// See https://aka.ms/new-console-template for more information
using System.Text.Json;

Console.WriteLine("Hello, World!");


// https://c-sharptutorial.com/basic/basic-example-in-csharp

DateTime dateTime = new DateTime(2023, 10, 31, 14, 30, 0);
Console.WriteLine("Datetime is :{0}", dateTime);
DateTime currentDateTime = DateTime.Now;
Console.WriteLine("Current DateTime is :{0}", currentDateTime);
string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
Console.WriteLine("Formatted datetime is :{0}", formattedDateTime);
int year = currentDateTime.Year;
int month = currentDateTime.Month;
int day = currentDateTime.Day;
int hour = currentDateTime.Hour;
int minute = currentDateTime.Minute;
int second = currentDateTime.Second;
Console.WriteLine("Year is :{0}", year);
Console.WriteLine("Month is :{0}", month);
Console.WriteLine("Day is :{0}", day);
Console.WriteLine("Hour is :{0}", hour);
Console.WriteLine("Minute is :{0}", minute);
Console.WriteLine("Second is :{0}", second);
DateTime futureDateTime = currentDateTime.AddHours(3);
DateTime pastDateTime = currentDateTime.AddMonths(-2);
Console.WriteLine("Future datetime is :{0}", futureDateTime);
Console.WriteLine("Past datetime is :{0}", pastDateTime);
bool isBefore = currentDateTime < futureDateTime;
Console.WriteLine("Future datetime is greater then past datetime :{0}", isBefore);
var dateTimeOffset = new DateTimeOffset(new DateTime(2023, 5, 15, 7, 0, 0), new TimeSpan(-7, 0, 0));
Console.WriteLine("DateTime Offset is :{0}", dateTimeOffset);
string dateStr = "2023-10-31";
DateTime parsedDate = DateTime.Parse(dateStr);
Console.WriteLine("Parse datetime is :{0}", parsedDate);
DateTime startTime = DateTime.Now;
DateTime endTime = DateTime.Now.AddSeconds(75);
TimeSpan span = endTime.Subtract(startTime);
Console.WriteLine("Time Difference (seconds): " + span.Seconds);
Console.WriteLine("Time Difference (minutes): " + span.Minutes);
Console.WriteLine("Time Difference (hours): " + span.Hours);
Console.WriteLine("Time Difference (days): " + span.Days);

// --------------------------

DateOnly date = new DateOnly(2023, 10, 31);
int year1 = date.Year; // 2023
int month1 = date.Month; // 10
int day1 = date.Day; // 31
Console.WriteLine("Year is = {0}", year1);
Console.WriteLine("Month is = {0}", month1);
Console.WriteLine("Day is = {0}", day1);
string defaultFormatted = date.ToString(); // "2023-10-31"
string customFormatted = date.ToString("MM/dd/yyyy"); // "10/31/2023"
Console.WriteLine("The default formated date is = {0}", defaultFormatted);
Console.WriteLine("The custom formated date is = {0}", customFormatted);
DateOnly tomorrow = date.AddDays(1); // Add one day;
DateOnly yesterday = date.AddDays(-1); // Subtract one day
bool isGreaterThan = tomorrow > yesterday; // Compare two DateOnly instances
Console.WriteLine("The tomorrow date is = {0}", tomorrow);
Console.WriteLine("The yesterday date is = {0}", yesterday);
Console.WriteLine("Is tomorrow greater yesterday = {0}", isGreaterThan);


