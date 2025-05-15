using System.Globalization;
using System.Linq;
using System.Text;

namespace WeekAndDays.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayOfWeek weekStart = DayOfWeek.Monday;
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            DateTime previousWeekStart = startingDate.AddDays(-7);
            DateTime previousWeekEnd = startingDate.AddDays(-1);

            Console.WriteLine(previousWeekStart);
            Console.WriteLine(previousWeekEnd);


            //--------------------------
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);

            Console.WriteLine(currentDay);
            Console.WriteLine(currentWeekStartDate);

            //--------------------------
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Displays the number of the current week relative to the beginning of the year.
            Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
            Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
            Console.WriteLine("Therefore, the current week is Week {0} of the current year.", myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW));

            // Displays the total number of weeks in the current year.
            DateTime LastDay = new System.DateTime(DateTime.Now.Year, 12, 31);
            Console.WriteLine("There are {0} weeks in the current year ({1}).", myCal.GetWeekOfYear(LastDay, myCWR, myFirstDOW), LastDay.Year);


            ////////--------------------------
            //////Console.WriteLine("\t Program to get Day Of the Week After Specified Days");
            //////Console.WriteLine("\t ==================================================================");
            //////Console.Write("\t Enter Days After Today : ");
            //////int days = Convert.ToInt32(Console.ReadLine());
            //////DateTime date = DateTime.Now.AddDays(days);
            //////Console.WriteLine("\t Current Date : " + DateTime.Now.ToString("MM/dd/yyyy"));
            //////Console.WriteLine("\t Date After {0} Days : {1}", days, date.ToString("MM/dd/yyyy"));
            //////Console.WriteLine("\t Day Of The Week On {0} : {1}", date.ToString("MM/dd/yyyy"), date.DayOfWeek);
            //////Console.ReadKey();


            ////////--------------------------
            //////DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            //////DateTime date1 = new DateTime(2023, 8, 3);
            //////Calendar cal = dfi.Calendar;

            //////Console.WriteLine("{0:d}: Week {1} ({2})", date1,
            //////                  cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,dfi.FirstDayOfWeek),
            //////                  cal.ToString().Substring(cal.ToString().LastIndexOf(".") + 1));


            ////////------------------------
            //////foreach (WeekRange wr in GetWeekRange(new DateTime(2023, 08, 03), new DateTime(2023, 09, 15)))
            //////{
            //////    Console.WriteLine("{0} {1} {2} {3}", wr.WeekNo, wr.MM, wr.Start.Date.ToShortDateString(), wr.End.ToShortDateString());
            //////}
            //////Console.ReadLine();


            //------------------------

            DateTime reference = DateTime.Now;
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;

            IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

            List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                .ToList();

            weeks.ForEach(x => Console.WriteLine("{0:MM/dd/yyyy} - {1:MM/dd/yyyy}", x.Item1, x.Item2));

            //------------------------



            Console.WriteLine("PreviousMonday: " + GetPreviousMonday(DateTime.Now).ToString());
            Console.WriteLine("CurrentMonday: " + GetCurrentMonday(DateTime.Now).ToString());
            Console.WriteLine("NextMonday: " + GetNextMonday(DateTime.Now).ToString());
            Console.WriteLine("EndOfCurrentWeek: " + GetEndOfCurrentWeek(DateTime.Now).ToString());
            Console.WriteLine("EndOfPreviousWeek: " + GetEndOfPreviousWeek(DateTime.Now).ToString());
            Console.WriteLine("EndOfNextWeek: " + GetEndOfNextWeek(DateTime.Now).ToString());

            Console.WriteLine("FindFirstNextDay: " + FindFirstNextDay(DateTime.Now, DayOfWeek.Tuesday).ToString());

            //------------------------------

            var firstDate = FirstDateOfWeek(2023, 31);
            var allWeekDays = new List<DateTime>();
            allWeekDays.Add(firstDate);
            var currentDate = firstDate;
            for (int d = 1; d < 7; d++)
            {
                currentDate = currentDate.AddDays(1);
                allWeekDays.Add(currentDate);
            }
            // or
            var week = Enumerable.Range(0, 7).Select(d => firstDate.AddDays(d)).ToList();
            Console.WriteLine(week[2]);

            //----------------------------

            var d1 = new DateTime(2023, 8, 1);
            var d2 = new DateTime(2023, 8, 25);
            var currentCulture = CultureInfo.CurrentCulture;
            var weeks11 = new List<int>();

            for (var dt = d1; dt < d2; dt = dt.AddDays(1))
            {
                var weekNo = currentCulture.Calendar.GetWeekOfYear(
                                      dt,
                                      currentCulture.DateTimeFormat.CalendarWeekRule,
                                      currentCulture.DateTimeFormat.FirstDayOfWeek);
                if (!weeks11.Contains(weekNo))
                    weeks11.Add(weekNo);
            }
            Console.WriteLine("week count between d1 and d2 is : " + weeks11.Count().ToString());

            //////var weeks22 = Weeks22(Convert.ToDateTime(d1), Convert.ToDateTime(d2));
            //////Console.WriteLine("week count between d1 and d2 is : " + weeks22.Count().ToString());


            //----------------------------
            var weekNumber = new HashSet<int>();
            var end = new DateTime(2023, 8, 25);
            var calendar11 = new GregorianCalendar();
            for (var date = new DateTime(2023, 8, 1); date <= end; date = date.AddDays(1))
            {
                weekNumber.Add(calendar11.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday));
            }
            Console.WriteLine("week count between d1 and d2 is : " + weekNumber.Count().ToString());


            //----------------------------

            Calendar Calendar = CultureInfo.InvariantCulture.Calendar;
            int weekNumber11 = Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            Console.WriteLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
            Console.WriteLine(weekNumber11);

            //----------------------------

            MyWeek week33 = new MyWeek(DateTime.Now);
            Console.WriteLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
            Console.WriteLine("Week: " + week33.WeekNumber);
            Console.WriteLine("Starts on: " + week33.FirstDateOfWeek.ToString("yyyy-MM-dd"));
            Console.WriteLine("Ends on: " + week33.LastDateOfWeek.ToString("yyyy-MM-dd"));
            Console.WriteLine("In year: " + week33.WeekYear);


            //----------------------------

            // Use a DayOfWeek list.
            List<DayOfWeek> workDays = new List<DayOfWeek>();
            workDays.Add(DayOfWeek.Monday);
            workDays.Add(DayOfWeek.Wednesday);

            // Loop over list of days.
            foreach (var day in workDays)
            {
                Console.WriteLine($"WORK DAY: {day}");
            }

            //----------------------------


            string startDay = myFirstDOW.ToString();
            DateTime dayInWeek = DateTime.Now;
            Tuple<DateTime, DateTime> academicWeek = CampusVueDateRange(dayInWeek, startDay);

            Console.WriteLine("Default First Day of Week: {0}", GetFirstDayOfWeek(DateTime.Now));
            Console.WriteLine("Specified First Day of Week: {0}", GetFirstDayOfWeek(dayInWeek, startDay));
            Console.WriteLine("Default Last Day of Week: {0}", GetLastDayOfWeek(dayInWeek));
            Console.WriteLine("Specified Last Day of Week: {0}", GetLastDayOfWeek(dayInWeek, startDay));
            Console.WriteLine("This academic week is from {0} to {1}", academicWeek.Item1, academicWeek.Item2);







            Console.ReadKey();
        }

        //
        public static IEnumerable<WeekRange> GetWeekRange(DateTime dtStart, DateTime dtEnd)
        {
            DateTime fWeekStart, dt, fWeekEnd;
            int wkCnt = 1;

            if (dtStart.DayOfWeek != DayOfWeek.Sunday)
            {
                fWeekStart = dtStart.AddDays(7 - (int)dtStart.DayOfWeek);
                fWeekEnd = fWeekStart.AddDays(-1);
                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(dtStart, fWeekEnd, dtStart.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;

            }
            else
            {
                fWeekStart = dtStart;
            }


            for (dt = fWeekStart.AddDays(6); dt <= dtEnd; dt = dt.AddDays(7))
            {


                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(fWeekStart, dt, fWeekStart.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;
                fWeekStart = dt.AddDays(1);


            }

            if (dt > dtEnd)
            {

                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(fWeekStart, dtEnd, dtEnd.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;

            }

        }

        public static IEnumerable<WeekRange> getMonthRange(WeekRange weekRange)
        {

            List<WeekRange> ranges = new List<WeekRange>();

            if (weekRange.Start.Month != weekRange.End.Month)
            {
                DateTime lastDayOfMonth = new DateTime(weekRange.Start.Year, weekRange.Start.Month, 1).AddMonths(1).AddDays(-1);
                ranges.Add(new WeekRange(weekRange.Start, lastDayOfMonth, weekRange.Start.Month, weekRange.WeekNo));
                ranges.Add(new WeekRange(lastDayOfMonth.AddDays(1), weekRange.End, weekRange.End.Month, weekRange.WeekNo + 1));

            }
            else
            {
                ranges.Add(weekRange);
            }

            return ranges;

        }


        //public List<weekrang> WeekDays(DateTime startDate, DateTime endDate)
        //{
        //    DateTime startDateToCheck = startDate;
        //    DateTime dateToCheck = startDate;
        //    DateTime dateRangeBegin = dateToCheck;
        //    DateTime dateRangeEnd = endDate;

        //    List<weekrange> weekRangeList = new List<weekrange>();
        //    WeekRange1 weekRange = new WeekRange1();


        //    while ((startDateToCheck.Year <= endDate.Year) && (startDateToCheck.Month <= endDate.Month) && dateToCheck <= endDate)
        //    {
        //        int week = 0;

        //        while (startDateToCheck.Month == dateToCheck.Month && dateToCheck <= endDate)
        //        {
        //            week = week + 1;
        //            dateRangeBegin = dateToCheck.AddDays(-(int)dateToCheck.DayOfWeek);
        //            dateRangeEnd = dateToCheck.AddDays(6 - (int)dateToCheck.DayOfWeek);

        //            if ((dateRangeBegin.Date < dateToCheck) && (dateRangeBegin.Date.Month != dateToCheck.Month))
        //            {
        //                dateRangeBegin = new DateTime(dateToCheck.Year, dateToCheck.Month, dateToCheck.Day);
        //            }

        //            if ((dateRangeEnd.Date > dateToCheck) && (dateRangeEnd.Date.Month != dateToCheck.Month))
        //            {
        //                DateTime dtTo = new DateTime(dateToCheck.Year, dateToCheck.Month, 1);
        //                dtTo = dtTo.AddMonths(1);
        //                dateRangeEnd = dtTo.AddDays(-(dtTo.Day));
        //            }
        //            if (dateRangeEnd.Date > endDate)
        //            {
        //                dateRangeEnd = new DateTime(dateRangeEnd.Year, dateRangeEnd.Month, endDate.Day);
        //            }
        //            weekRange = new WeekRange1
        //            {
        //                StartDate = dateRangeBegin,
        //                EndDate = dateRangeEnd,
        //                Range = dateRangeBegin.Date.ToShortDateString() + '-' + dateRangeEnd.Date.ToShortDateString(),
        //                Month = dateToCheck.Month,
        //                Year = dateToCheck.Year,
        //                Week = week
        //            };
        //            weekRangeList.Add(weekRange);
        //            dateToCheck = dateRangeEnd.AddDays(1);
        //        }
        //        startDateToCheck = startDateToCheck.AddMonths(1);
        //    }

        //    return weekRangeList;
        //}


        public static bool SundayIsFirstDayOfWeek { get; set; } = false;

        public static DateTime GetCurrentMonday(DateTime date)
        {
            int diff = DayOfWeek.Monday - date.DayOfWeek;
            if (date.DayOfWeek == DayOfWeek.Sunday && !SundayIsFirstDayOfWeek)
            {
                diff -= 7;
            }
            return DateTime.SpecifyKind(date.Date.AddDays(diff), DateTimeKind.Unspecified);
        }

        public static DateTime GetPreviousMonday(DateTime date) => GetCurrentMonday(date).AddDays(-7);
        public static DateTime GetNextMonday(DateTime date) => GetCurrentMonday(date).AddDays(7);
        public static DateTime GetEndOfCurrentWeek(DateTime date) => GetCurrentMonday(date).AddDays(5);
        public static DateTime GetEndOfPreviousWeek(DateTime date) => GetPreviousMonday(date).AddDays(5);
        public static DateTime GetEndOfNextWeek(DateTime date) => GetNextMonday(date).AddDays(5);

        public static DateTime FindFirstNextDay(DateTime date, DayOfWeek wantedDayOfWeek)
        {
            int diff = wantedDayOfWeek - date.DayOfWeek;
            return date.Date.AddDays(diff < 1 ? diff + 7 : diff);
        }

        //
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = Convert.ToInt32(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) - Convert.ToInt32(jan1.DayOfWeek);
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            System.Globalization.CultureInfo curCulture = System.Globalization.CultureInfo.CurrentCulture;
            int firstWeek = curCulture.Calendar.GetWeekOfYear(jan1, curCulture.DateTimeFormat.CalendarWeekRule, curCulture.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }


        public static List<int> Weeks22(DateTime start, DateTime end)
        {
            List<int> weeks = new List<int>();
            var Week = (int)Math.Floor((double)start.DayOfYear / 7.0); //starting week number
            for (DateTime t = start; t < end; t = t.AddDays(7))
            {
                weeks.Add(Week);
                Week++;
            }
            return weeks;
        }

        //


        /// <summary>
        /// Returns the first day of the week that the specified date is in
        /// using the current culture.
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;

            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, string startDay)
        {
            DayOfWeek firstDay = ParseEnum<DayOfWeek>(startDay);
            return GetFirstDayOfWeek(dayInWeek, firstDay);
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            return GetFirstDayOfWeek(dayInWeek, firstDay);
        }

        private static DateTime GetLastDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetLastDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        private static DateTime GetLastDayOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, firstDay);
            return firstDayInWeek.AddDays(7);
        }

        private static DateTime GetLastDayOfWeek(DateTime dayInWeek, string startDay)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, startDay);
            return firstDayInWeek.AddDays(7);
        }

        private static DateTime GetLastDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DateTime firstDayInWeek = GetFirstDayOfWeek(dayInWeek, cultureInfo);
            return firstDayInWeek.AddDays(7);
        }

        private static Tuple<DateTime, DateTime> CampusVueDateRange(DateTime dayInWeek, string startDay)
        {
            DateTime firstDayOfWeek = GetFirstDayOfWeek(dayInWeek, startDay).AddSeconds(1);
            DateTime lastDayOfWeek = GetLastDayOfWeek(dayInWeek, startDay);

            return new Tuple<DateTime, DateTime>(firstDayOfWeek, lastDayOfWeek);
        }

        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        //
    }


    struct WeekRange
    {
        public DateTime Start;
        public DateTime End;
        public int MM;
        public int WeekNo;

        public WeekRange(DateTime _start, DateTime _end, int _mm, int _weekNo)
        {
            Start = _start;
            End = _end;
            MM = _mm;
            WeekNo = _weekNo;
        }

    }

    //public class WeekRange1
    //{
    //    public string Range { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public int Week { get; set; }
    //    public int Month { get; set; }
    //    public int Year { get; set; }
    //}


}





