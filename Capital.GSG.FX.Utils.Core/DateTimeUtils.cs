using System;
using System.Collections.Generic;
using System.Linq;

namespace Capital.GSG.FX.Utils.Core
{
    public static class DateTimeUtils
    {
        public static DateTime GetFromUnixTimeStamp(long unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static DateTimeOffset GetFivePmYesterday(ITimeProvider timeProvider = null)
        {
            if (timeProvider == null)
                timeProvider = new SystemTimeProvider();

            DateTime today = timeProvider.Today().ToUniversalTime();

            DateTime fivePm = new DateTime(today.Year, today.Month, today.Day, 17, 0, 0);

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeSpan offset = estTz.GetUtcOffset(fivePm);

            DateTimeOffset dto = new DateTimeOffset(fivePm, offset);

            if (dto.UtcDateTime > timeProvider.Now().UtcDateTime)
                dto = dto.AddDays(-1);

            return dto.ToLocalTime();
        }

        /// <summary>
        /// Computes the delay between 2 DateTime objects and eveluates if it is smaller than the max difference.
        /// The order of the two datetime objects doesn't matter
        /// </summary>
        /// <param name="ts1">The first datetime</param>
        /// <param name="ts2">The second datetime</param>
        /// <param name="maxDiffMs">The maximum difference allowed, in  milliseconds</param>
        /// <returns></returns>
        public static bool HaveSameTimeStamp(DateTime ts1, DateTime ts2, double maxDiffMs)
        {
            if (ts1 == ts2)
                return true;
            else if (ts1 < ts2)
                return ts2.Subtract(ts1) <= TimeSpan.FromMilliseconds(maxDiffMs);
            else if (ts1 > ts2)
                return ts1.Subtract(ts2) <= TimeSpan.FromMilliseconds(maxDiffMs);
            else
                return false;
        }

        /// <summary>
        /// Gets the lower (item1) and upper (item2) boundaries of a trading date specified in HKT. Starts at 5am in summer and 6 am in winter
        /// </summary>
        /// <param name="dateInHKT"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetTradingDayBoundaries(DateTime dateInHKT)
        {
            DateTime date = dateInHKT.Date;

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            if (estTz.IsDaylightSavingTime(date)) // summer - 5am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 5, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 5, 0, 0, DateTimeKind.Local)).AddDays(1));
            else // winter - 6am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 6, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 6, 0, 0, DateTimeKind.Local)).AddDays(1));
        }

        public static Tuple<DateTimeOffset, DateTimeOffset> GetTradingDayBoundariesDateTimeOffset(DateTime dateInHKT)
        {
            DateTime startDate = dateInHKT.Date.AddDays(-1); // The "trading day" begins at 5pm NYT on the day before. eg: TD 23AUG begins at 22AUG 5pm EDT
            DateTime endDate = dateInHKT.Date;

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            return new Tuple<DateTimeOffset, DateTimeOffset>(
                new DateTimeOffset(startDate.Year, startDate.Month, startDate.Day, 17, 0, 0, estTz.GetUtcOffset(startDate)),
                new DateTimeOffset(endDate.Year, endDate.Month, endDate.Day, 17, 0, 0, estTz.GetUtcOffset(endDate)));
        }

        public static TimeSpan GetNewYorkTimeUtcOffset(this DateTime dateInHKT)
        {
            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            return estTz.GetUtcOffset(dateInHKT);
        }

        /// <summary>
        /// Gets the lower (item1) and upper (item2) boundaries of a NZD trading date specified in HKT. Starts at 2am in HK winter and 3am in HK summer
        /// </summary>
        /// <param name="dateInHKT"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetNzdTradingDayBoundaries(DateTime dateInHKT)
        {
            DateTime date = dateInHKT.Date;

            TimeZoneInfo nzstTz = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

            if (nzstTz.IsDaylightSavingTime(date)) // NZ summer / HK winter - 2am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 2, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 2, 0, 0, DateTimeKind.Local)).AddDays(1));
            else // NZ winter / HK summer - 3am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 3, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 3, 0, 0, DateTimeKind.Local)).AddDays(1));
        }

        public static Tuple<DateTimeOffset, DateTimeOffset> GetNzdTradingDayBoundariesDateTimeOffset(DateTime dateInHKT)
        {
            DateTime startDate = dateInHKT.Date;
            DateTime endDate = dateInHKT.Date.AddDays(1);

            TimeZoneInfo nzstTz = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

            // NZD rolls at 7am Auckland time
            return new Tuple<DateTimeOffset, DateTimeOffset>(
                new DateTimeOffset(startDate.Year, startDate.Month, startDate.Day, 7, 0, 0, nzstTz.GetUtcOffset(startDate)),
                new DateTimeOffset(endDate.Year, endDate.Month, endDate.Day, 7, 0, 0, nzstTz.GetUtcOffset(endDate)));
        }

        /// <summary>
        /// This method takes a date range (with a lower bound and an upper bound) and returns an IEnumerable<DateTime> containing each day in the range
        /// </summary>
        /// <param name="from">The lower bound</param>
        /// <param name="thru">The upper bound</param>
        /// <returns>An IEnumerable<DateTime> containing each day in the range</returns>
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static IEnumerable<DateTime> EachBusinessDay(DateTime from, DateTime thru)
        {
            var days = EachDay(from, thru);

            return days?.Where(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday);
        }

        /// <summary>
        /// Floors the date to the nearest unit defined by span
        /// eg: span = TimeSpan(0, 0, 1) // 1 second
        /// => will return date floored to 1 second
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static DateTimeOffset Floor(this DateTimeOffset date, TimeSpan span)
        {
            long ticks = date.Ticks / span.Ticks;

            return new DateTimeOffset(ticks * span.Ticks, date.Offset);
        }


        /// <summary>
        /// Rounds (up to midpoint) the date to the nearest unit defined by span
        /// eg: span = TimeSpan(0, 0, 1) // 1 second
        /// => will return date rounded to 1 second
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static DateTime Round(this DateTime date, TimeSpan span)
        {
            long ticks = (date.Ticks + (span.Ticks / 2) + 1) / span.Ticks;

            return new DateTime(ticks * span.Ticks);
        }

        public static DateTimeOffset Round(this DateTimeOffset date, TimeSpan span)
        {
            long ticks = (date.Ticks + (span.Ticks / 2) + 1) / span.Ticks;

            return new DateTimeOffset(ticks * span.Ticks, date.Offset);
        }

        /// <summary>
        /// Ceils the date to the nearest unit defined by span
        /// eg: span = TimeSpan(0, 0, 1) // 1 second
        /// => will return date ceiled to 1 second
        /// </summary>
        /// <param name="date"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static DateTime Ceiling(this DateTime date, TimeSpan span)
        {
            long ticks = (date.Ticks + span.Ticks - 1) / span.Ticks;

            return new DateTime(ticks * span.Ticks);
        }

        public static DateTime GetLastBusinessDayInHKT()
        {
            DateTime day = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(8)).Date;

            if (day.DayOfWeek == DayOfWeek.Saturday)
                day = day.AddDays(-1);
            else if (day.DayOfWeek == DayOfWeek.Sunday)
                day = day.AddDays(-2);

            return day;
        }
    }
}
