using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Teirlinck.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsTests
{
    [TestClass]
    public class ArrayUtilsTests
    {
        [TestMethod]
        public void TestSubArray()
        {
            int[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Good case 1
            try
            {
                int[] sub = data.SubArray(3, 4); // contains {3,4,5,6}

                Assert.IsTrue(sub.Length == 4);
                Assert.IsTrue(sub[0] == 3);
                Assert.IsTrue(sub[3] == 6);
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message);
            }

            // Good case 2
            try
            {
                int[] sub = data.SubArray(4); // contains {4,5,6,7,8,9}

                Assert.IsTrue(sub.Length == 6);
                Assert.IsTrue(sub[0] == 4);
                Assert.IsTrue(sub[5] == 9);
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message);
            }

            // Bad case: sourceArray
            try
            {
                int[] sub = (new int[0] { }).SubArray(-1, 4);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message == "sourceArray");
            }

            // Bad case: index
            try
            {
                int[] sub = data.SubArray(-1, 4);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message == "index");
            }

            // Bad case: length
            try
            {
                int[] sub = data.SubArray(3, 8);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message == "length");
            }
        }

        [TestMethod]
        public void TestSubArray2()
        {
            double[] test = new double[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN };

            int firstMark = 10;
            int secondMark = 20;

            Assert.IsTrue(test.SubArray(1, firstMark).Length == 10);
            Assert.IsTrue(test.SubArray(1, firstMark)[0] == 1);
            Assert.IsTrue(test.SubArray(1, firstMark)[9] == 10);

            Assert.IsTrue(test.SubArray(1, secondMark).Length == 20);
            Assert.IsTrue(test.SubArray(1, secondMark)[0] == 1);
            Assert.IsTrue(double.IsNaN(test.SubArray(1, secondMark)[19]));
        }
    }
}
