﻿using System;

namespace Net.Teirlinck.Utils
{
    public static class DateTimeUtils
    {
        private static ITimeProvider timeProvider;
        public static ITimeProvider TimeProvider
        {
            get
            {
                if (timeProvider == null)
                    timeProvider = new CurrentTimeProvider();

                return timeProvider;
            }

            set
            {
                timeProvider = value;
            }
        }

        public static DateTime Now()
        {
            return TimeProvider.Now();
        }

        public static DateTime Today()
        {
            return TimeProvider.Now().Date;
        }

        public static DateTime GetFromUnixTimeStamp(long unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return dt.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static DateTime GetFivePmYesterday()
        {
            DateTime today = Today().ToUniversalTime();

            DateTime fivePm = new DateTime(today.Year, today.Month, today.Day, 17, 0, 0);

            TimeZoneInfo estTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeSpan offset = estTz.GetUtcOffset(fivePm);

            DateTimeOffset dto = new DateTimeOffset(fivePm, offset);

            if (dto.LocalDateTime > Now())
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
    }
}
