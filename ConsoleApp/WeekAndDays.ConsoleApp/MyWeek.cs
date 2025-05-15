using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekAndDays.ConsoleApp
{
    public class MyWeek : IComparable<MyWeek>, IEquatable<MyWeek>
    {
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
        private const DayOfWeek FirstDayOfYear = DayOfWeek.Thursday;
        private static readonly Calendar Calendar = CultureInfo.InvariantCulture.Calendar;

        public MyWeek(DateTime date)
        {
            FirstDateOfWeek = GetFirstDateOfWeek(date);
            LastDateOfWeek = GetLastDateOfWeek(date);
            DateTime offsetDate = GetYearOffsetDayOfWeek(FirstDateOfWeek);
            WeekNumber = Calendar.GetWeekOfYear(offsetDate, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
            WeekYear = offsetDate.Year;
        }

        public DateTime FirstDateOfWeek { get; private set; }

        public DateTime LastDateOfWeek { get; private set; }

        public int WeekNumber { get; private set; }

        public int WeekYear { get; private set; }

        public int CompareTo(MyWeek week)
        {
            return week == null ? -1 : string.Compare(ToString(), week.ToString(), StringComparison.Ordinal);
        }

        public bool Equals(MyWeek week)
        {
            return week != null && string.Equals(ToString(), week.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a sortable ISO week string (e.g. "2023-W22").
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}-W{1:00}", WeekYear, WeekNumber);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MyWeek);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        private DateTime GetFirstDateOfWeek(DateTime date)
        {
            while (date.DayOfWeek != FirstDayOfWeek)
                date = date.AddDays(-1);
            return date;
        }

        private DateTime GetLastDateOfWeek(DateTime date)
        {
            while (date.DayOfWeek != LastDayOfWeek)
                date = date.AddDays(1);
            return date;
        }

        private DateTime GetYearOffsetDayOfWeek(DateTime firstDateOfWeek)
        {
            while (firstDateOfWeek.DayOfWeek != FirstDayOfYear)
                firstDateOfWeek = firstDateOfWeek.AddDays(1);
            return firstDateOfWeek;
        }
    }
}
