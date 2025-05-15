using System.Globalization;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        string strInputWeekNums = "33,35-39,45";
        //string strInputWeekNums = "33,36,37";
        //string strInputWeekNums = "35-38";

        string[] strWeekNums = strInputWeekNums.Split(',');

        DayOfWeek FindthisDay = DayOfWeek.Tuesday; // <<<<< to find the Day from week

        List<DateTime> resultXDays = new List<DateTime>();

        //--------------------------
        //Weeks are according Singapore calendar rules, Sunday first day and weeks are Sunday to Saturday
        //https://savvytime.com/week-number/singapore/2023
        //https://learn.microsoft.com/en-us/dotnet/api/system.globalization.calendar.getweekofyear?view=net-6.0
        // Gets the Calendar instance associated with a CultureInfo.
        CultureInfo myCI = new CultureInfo("en-US"); //en-SG
        Calendar myCal = myCI.Calendar;

        // Gets the DTFI properties required by GetWeekOfYear.
        CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

        // Displays the number of the current week relative to the beginning of the year.
        Console.WriteLine("The CalendarWeekRule used for the en-US culture is [{0}].", myCWR);
        Console.WriteLine("The FirstDayOfWeek used for the en-US culture is [{0}].", myFirstDOW);
        Console.WriteLine("Therefore, the current week is Week [{0}] of the current year.", myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW));

        // Displays the total number of weeks in the current year.
        DateTime LastDay = new DateTime(DateTime.Now.Year, 12, 31);
        int NumOfWeeksInCurrentYear = myCal.GetWeekOfYear(LastDay, myCWR, myFirstDOW);
        Console.WriteLine("There are [{0}] weeks in the current year ({1}).", NumOfWeeksInCurrentYear, LastDay.Year);

        Console.WriteLine("...");

        // Current Year
        int intYear = LastDay.Year;
        string pointhere = "<<<<<";

        Console.WriteLine("week and week range for processing : " + strInputWeekNums);
        Console.WriteLine("...");

        foreach (var x in strWeekNums)
        {
            Console.WriteLine($"<{x.Trim()}>");

            // check week no. or week range
            if(x.All(char.IsDigit))
            {
                // >>>>> this is week number.
                int x1 = Convert.ToInt16(x.Trim());
                Console.WriteLine("True > this is week number [{0}] and Find this {1}'s Date from this Week No. [{0}].", x1, FindthisDay);
                
                //---------------------
                var firstDate = FirstDateOfWeek(intYear, x1);
                var datefromweek = Enumerable.Range(0, 7).Select(d => firstDate.AddDays(d)).ToList();

                foreach(var y in datefromweek)
                {
                    if(y.DayOfWeek == FindthisDay)
                    {
                        Console.WriteLine("Week [{0}], Day Of The Date {1} is : {2} {3}", x1, y.ToString("yyyy-MM-dd"), y.DayOfWeek, pointhere);
                        resultXDays.Add(Convert.ToDateTime(y.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        Console.WriteLine("Week [{0}], Day Of The Date {1} is : {2}", x1, y.ToString("yyyy-MM-dd"), y.DayOfWeek);
                    }
                }
                Console.WriteLine("...");
                //---------------------
            }
            else
            {
                // >>>>> this is week range.
                string r1 = x.Trim();
                Console.WriteLine("False > this is week range for ({0}) and Find this {1}'s Date from all those weeks.", r1, FindthisDay);
                string[] strWeekNumList = r1.Split('-');

                //int step = 1;
                //IEnumerable<int> XNumbers = XInt32(Convert.ToInt32(strWeekNumList[0]), Convert.ToInt32(strWeekNumList[1]), step);
                IEnumerable<int> XNumbers = Enumerable.Range(Convert.ToInt32(strWeekNumList[0]), Convert.ToInt32(strWeekNumList[1]) - Convert.ToInt32(strWeekNumList[0]) + 1);

                foreach (var x1 in XNumbers)
                {
                    Console.WriteLine($"<{x1}>");

                    //---------------------
                    var firstDate = FirstDateOfWeek(intYear, x1);
                    var datefromweek = Enumerable.Range(0, 7).Select(d => firstDate.AddDays(d)).ToList();

                    foreach (var y in datefromweek)
                    {
                        if (y.DayOfWeek == FindthisDay)
                        {
                            Console.WriteLine("Week [{0}], Day Of The Date {1} is : {2} {3}", x1, y.ToString("yyyy-MM-dd"), y.DayOfWeek, pointhere);
                            resultXDays.Add(Convert.ToDateTime(y.ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            Console.WriteLine("Week [{0}], Day Of The Date {1} is : {2}", x1, y.ToString("yyyy-MM-dd"), y.DayOfWeek);
                        }
                    }
                    Console.WriteLine("...");
                    //---------------------
                }

            }


        }

        Console.WriteLine("... Result ...");
        if (resultXDays.Count > 0)
        {
            foreach(var z in resultXDays)
            {
                Console.WriteLine("Selected {0}'s Date : {1}", FindthisDay, z.ToString("dd/MM/yyyy"));
            }
        }


        Console.WriteLine("... ... ... ...");
        // Demonstrate all DayOfWeek values.
        Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}",
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday);

        Console.ReadKey();
    }


    //
    public static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = Convert.ToInt32(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) - Convert.ToInt32(jan1.DayOfWeek);
        DateTime firstWeekDay = jan1.AddDays(daysOffset);
        CultureInfo curCulture = CultureInfo.CurrentCulture;
        int firstWeek = curCulture.Calendar.GetWeekOfYear(jan1, curCulture.DateTimeFormat.CalendarWeekRule, curCulture.DateTimeFormat.FirstDayOfWeek);
        if (firstWeek <= 1)
        {
            weekOfYear -= 1;
        }
        return firstWeekDay.AddDays(weekOfYear * 7);
    }

    public static DateTime FirstDayOfWeek(DateTime dt)
    {
        //DateTime d = new DateTime(yr, mn, dt);
        //Console.WriteLine(" The formatted Date is : {0}", d.ToString("dd/MM/yyyy"));
        //Console.WriteLine(" The first day of the week for the above date is : {0}\n", FirstDayOfWeek(d).ToString("dd/MM/yyyy"));

        //
        var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        if (diff < 0)
            diff += 7;
        return dt.AddDays(-diff).Date;
    }

    public static IEnumerable<int> XInt32(int from, int to, int step)
    {
        if (step <= 0) step = (step == 0) ? 1 : -step;
        if (from <= to)
        {
            for (int i = from; i <= to; i += step) yield return i;
        }
        else
        {
            for (int i = from; i >= to; i -= step) yield return i;
        }
    }

    
}