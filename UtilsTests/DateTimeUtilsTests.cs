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
    }
}
