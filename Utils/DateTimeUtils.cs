using System;
using System.Collections.Generic;

namespace Net.Teirlinck.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetFromUnixTimeStamp(long unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static DateTime GetFivePmYesterday(ITimeProvider timeProvider = null)
        {
            if (timeProvider == null)
                timeProvider = new SystemTimeProvider();

            DateTime today = timeProvider.Today().ToUniversalTime();

            DateTime fivePm = new DateTime(today.Year, today.Month, today.Day, 17, 0, 0);

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeSpan offset = estTz.GetUtcOffset(fivePm);

            DateTimeOffset dto = new DateTimeOffset(fivePm, offset);

            if (dto.LocalDateTime > timeProvider.Now())
                dto = dto.AddDays(-1);

            return dto.LocalDateTime;
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

            if (estTz.IsDaylightSavingTime(date)) // winter - 6am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 6, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 6, 0, 0, DateTimeKind.Local)).AddDays(1));
            else // summer - 5am HKT
                return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, date.Day, 5, 0, 0, DateTimeKind.Local), (new DateTime(date.Year, date.Month, date.Day, 5, 0, 0, DateTimeKind.Local)).AddDays(1));
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
    }
}
