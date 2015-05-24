using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Teirlinck.Utils;

namespace UtilsTests
{
    [TestClass]
    public class DateTimeUtilsTests
    {
        [TestMethod]
        public void TestGetFivePmYesterday()
        {
            DateTime dt = DateTimeUtils.GetFivePmYesterday();

            Assert.IsNotNull(dt);
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
    }
}
