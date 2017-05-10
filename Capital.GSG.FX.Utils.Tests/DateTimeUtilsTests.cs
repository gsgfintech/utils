using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capital.GSG.FX.Utils.Core.Tests
{
    [TestClass]
    public class DateTimeUtilsTests
    {
        //[Ignore]
        [TestMethod]
        public void TestGetFivePmYesterday()
        {
            ITimeProvider timeProvider = new ManualTimeProvider();

            TimeZoneInfo easternTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); // used to run check on the time

            DateTimeOffset dt = DateTimeUtils.GetFivePmYesterday(timeProvider);

            Assert.IsNotNull(dt);

            // Test for edge case: between 12am and 5am local time
            timeProvider.SetCurrentTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, TimeSpan.FromHours(8), 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday(timeProvider);

            Assert.IsTrue(dt.Date == timeProvider.Today().AddDays(-1).Date, "Testing GetFivePmYesterday with current time set to 1am: expect 5pm date to be on the previous HKT date");

            if (easternTz.IsDaylightSavingTime(timeProvider.Now()))
                Assert.IsTrue(dt.Hour == 5, "Currently EDT. Expect 5pm EDT = 5am HKT");
            else
                Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test for between 5am to 11:59pm local time
            timeProvider.SetCurrentTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, TimeSpan.FromHours(8), 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday(timeProvider);

            Assert.IsTrue(dt.Date == timeProvider.Today().Date, "Testing GetFivePmYesterday with current time set to 1pm: expect 5pm date to be on the current HKT date");

            if (easternTz.IsDaylightSavingTime(timeProvider.Now()))
                Assert.IsTrue(dt.Hour == 5, "Currently EDT. Expect 5pm EDT = 5am HKT");
            else
                Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test in winter (Dec 15th)
            timeProvider.SetCurrentTime(DateTime.Now.Year, 12, 15, 13, TimeSpan.FromHours(8), 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday(timeProvider);

            Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test in summer (June 15th)
            timeProvider.SetCurrentTime(DateTime.Now.Year, 6, 15, 13, TimeSpan.FromHours(8), 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday(timeProvider);

            Assert.IsTrue(dt.Hour == 5, "Currently EDT. Expect 5pm EDT = 5am HKT");
        }

        [TestMethod]
        public void TestHaveSameTimeStamp()
        {
            double maxDiff = 500; // in milliseconds

            // Case #1: datetimes are equal
            DateTime dt1 = new DateTime(2015, 5, 23, 14, 41, 0, 500);
            DateTime dt2 = new DateTime(2015, 5, 23, 14, 41, 0, 500);

            Assert.IsTrue(DateTimeUtils.HaveSameTimeStamp(dt1, dt2, maxDiff));

            // Case #2: dt3 > dt4, outside of range (501ms)
            DateTime dt3 = new DateTime(2015, 5, 23, 14, 41, 1, 1);
            DateTime dt4 = new DateTime(2015, 5, 23, 14, 41, 0, 500);

            Assert.IsFalse(DateTimeUtils.HaveSameTimeStamp(dt3, dt4, maxDiff));

            // Case #3: dt5 < dt6, within range (500ms)
            DateTime dt5 = new DateTime(2015, 5, 23, 14, 41, 0, 500);
            DateTime dt6 = new DateTime(2015, 5, 23, 14, 41, 1, 0);

            Assert.IsTrue(DateTimeUtils.HaveSameTimeStamp(dt5, dt6, maxDiff));
        }

        [TestMethod]
        public void TestGetTradingDayBoundaries()
        {
            DateTime january = new DateTime(2015, 1, 15);

            Assert.IsTrue(DateTimeUtils.GetTradingDayBoundaries(january).Item1.Hour == 6);
            Assert.IsTrue(DateTimeUtils.GetNzdTradingDayBoundaries(january).Item1.Hour == 2);

            DateTime july = new DateTime(2015, 7, 15);

            Assert.IsTrue(DateTimeUtils.GetTradingDayBoundaries(july).Item1.Hour == 5);
            Assert.IsTrue(DateTimeUtils.GetNzdTradingDayBoundaries(july).Item1.Hour == 3);
        }

        [TestMethod]
        public void TestGetNewYorkTimeUtcOffset()
        {
            DateTime january = new DateTime(2015, 1, 15);

            Assert.IsTrue(DateTimeUtils.GetNewYorkTimeUtcOffset(january) == TimeSpan.FromHours(-5));

            DateTime july = new DateTime(2015, 7, 15);

            Assert.IsTrue(DateTimeUtils.GetNewYorkTimeUtcOffset(july) == TimeSpan.FromHours(-4));
        }

        [TestMethod]
        public void TestGetTradingDayBoundariesDateTimeOffset()
        {
            DateTime january = new DateTime(2015, 1, 15);

            var boundaries = DateTimeUtils.GetTradingDayBoundariesDateTimeOffset(january);

            Assert.IsTrue(boundaries.Item1.Offset == TimeSpan.FromHours(-5));
            Assert.IsTrue(boundaries.Item1.ToLocalTime().Hour == 6);

            boundaries = DateTimeUtils.GetNzdTradingDayBoundariesDateTimeOffset(january);

            Assert.IsTrue(boundaries.Item1.Offset == TimeSpan.FromHours(13));
            Assert.IsTrue(boundaries.Item1.ToLocalTime().Hour == 2);

            DateTime july = new DateTime(2015, 7, 15);

            boundaries = DateTimeUtils.GetTradingDayBoundariesDateTimeOffset(july);

            Assert.IsTrue(boundaries.Item1.Offset == TimeSpan.FromHours(-4));
            Assert.IsTrue(boundaries.Item1.ToLocalTime().Hour == 5);

            boundaries = DateTimeUtils.GetNzdTradingDayBoundariesDateTimeOffset(july);

            Assert.IsTrue(boundaries.Item1.Offset == TimeSpan.FromHours(12));
            Assert.IsTrue(boundaries.Item1.ToLocalTime().Hour == 3);
        }

        [TestMethod]
        public void TestEachDay()
        {
            DateTime lower = DateTime.Today.AddDays(-7);
            DateTime upper = DateTime.Today;

            IEnumerable<DateTime> days = DateTimeUtils.EachDay(lower, upper);

            Assert.IsTrue(days?.Count() == 8);

            days = null;
            days = DateTimeUtils.EachBusinessDay(lower, upper);

            Assert.IsTrue(days?.Count() <= 6);
        }

        [TestMethod]
        public void TestFloor()
        {
            DateTimeOffset date = new DateTimeOffset(2016, 7, 25, 7, 58, 45, 654, new TimeSpan(8, 0, 0));
            TimeSpan span = new TimeSpan(0, 0, 1);

            Assert.IsTrue(date.Floor(span) == new DateTime(2016, 7, 25, 7, 58, 45, 0));

            date = new DateTimeOffset(2016, 7, 25, 7, 58, 46, 654, new TimeSpan(8, 0, 0));
            span = new TimeSpan(0, 0, 5);

            Assert.IsTrue(date.Floor(span) == new DateTime(2016, 7, 25, 7, 58, 45, 0));

            date = new DateTimeOffset(2016, 7, 25, 7, 58, 49, 654, new TimeSpan(8, 0, 0));
            span = new TimeSpan(0, 0, 5);

            Assert.IsTrue(date.Floor(span) == new DateTime(2016, 7, 25, 7, 58, 45, 0));

            date = new DateTimeOffset(2016, 7, 25, 7, 58, 50, 654, new TimeSpan(8, 0, 0));
            span = new TimeSpan(0, 0, 5);

            Assert.IsTrue(date.Floor(span) == new DateTime(2016, 7, 25, 7, 58, 50, 0));
        }

        [TestMethod]
        public void TestRoun()
        {
            DateTime date = new DateTime(2016, 7, 25, 7, 58, 45, 654);
            TimeSpan span = new TimeSpan(0, 0, 1);

            Assert.IsTrue(date.Round(span) == new DateTime(2016, 7, 25, 7, 58, 46, 0));

            date = new DateTime(2016, 7, 25, 7, 58, 45, 454);

            Assert.IsTrue(date.Round(span) == new DateTime(2016, 7, 25, 7, 58, 45, 0));

            DateTimeOffset dto = new DateTimeOffset(2016, 7, 25, 7, 58, 45, 654, TimeSpan.FromHours(8));

            Assert.IsTrue(dto.Round(span) == new DateTimeOffset(2016, 7, 25, 7, 58, 46, 0, TimeSpan.FromHours(8)));

            dto = new DateTimeOffset(2016, 7, 25, 7, 58, 45, 454, TimeSpan.FromHours(8));

            Assert.IsTrue(dto.Round(span) == new DateTimeOffset(2016, 7, 25, 7, 58, 45, 0, TimeSpan.FromHours(8)));
        }

        [TestMethod]
        public void TestCeiling()
        {
            DateTime date = new DateTime(2016, 7, 25, 7, 58, 45, 454);
            TimeSpan span = new TimeSpan(0, 0, 1);

            Assert.IsTrue(date.Ceiling(span) == new DateTime(2016, 7, 25, 7, 58, 46, 0));
        }
    }
}
