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

            // Good case
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
                int[] sub = data.SubArray(3, 7);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message == "length");
            }
        }
    }
}
