using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Teirlinck.Utils;
using Moq;

namespace UtilsTests
{
    [TestClass]
    public class DateTimeUtilsTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Use current time provider as the default
            // Override in individual tests with SetCurrentTime if needed
            DateTimeUtils.TimeProvider = new CurrentTimeProvider();
        }

        [TestMethod]
        public void TestGetFivePmYesterday()
        {
            TimeZoneInfo easternTz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); // used to run check on the time

            DateTime dt = DateTimeUtils.GetFivePmYesterday();

            Assert.IsNotNull(dt);

            // Test for edge case: between 12am and 5am local time
            SetCurrentTime(1, 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday();

            Assert.IsTrue(dt.Date == DateTimeUtils.Today().AddDays(-1).Date, "Testing GetFivePmYesterday with current time set to 1am: expect 5pm date to be on the previous HKT date");

            if (easternTz.IsDaylightSavingTime(DateTimeUtils.Now()))
                Assert.IsTrue(dt.Hour == 5, "Currently EDT. Expect 5pm EDT = 5am HKT");
            else
                Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test for between 5am to 11:59pm local time
            SetCurrentTime(13, 0, 0);

            dt = DateTimeUtils.GetFivePmYesterday();

            Assert.IsTrue(dt.Date == DateTimeUtils.Today().Date, "Testing GetFivePmYesterday with current time set to 1pm: expect 5pm date to be on the current HKT date");

            if (easternTz.IsDaylightSavingTime(DateTimeUtils.Now()))
                Assert.IsTrue(dt.Hour == 5, "Currently EDT. Expect 5pm EDT = 5am HKT");
            else
                Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test in winter (Dec 15th)
            SetCurrentTime(new DateTime(DateTime.Now.Year, 12, 15, 13, 0, 0));

            dt = DateTimeUtils.GetFivePmYesterday();

            Assert.IsTrue(dt.Hour == 6, "Currently EDT. Expect 5pm EDT = 5am HKT");

            // Test in summer (June 15th)
            SetCurrentTime(new DateTime(DateTime.Now.Year, 6, 15, 13, 0, 0));

            dt = DateTimeUtils.GetFivePmYesterday();

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

        /// <summary>
        /// Use this to mock the current time
        /// </summary>
        /// <param name="currentTime">The current time we want to set to</param>
        private static void SetCurrentTime(DateTime currentTime)
        {
            Mock<ITimeProvider> mockTime = new Mock<ITimeProvider>();
            mockTime.Setup(t => t.Now()).Returns(currentTime);

            DateTimeUtils.TimeProvider = mockTime.Object;
        }

        private static void SetCurrentTime(int hours, int minutes = 0, int seconds = 0)
        {
            SetCurrentTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds));
        }
    }
}
