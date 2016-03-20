using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Teirlinck.Utils;

namespace UtilsTests
{
    [TestClass]
    public class NetworkUtilsTests
    {
        [TestMethod]
        public void TestIsProcessListening()
        {
            // 1. Test something that exists (FXMonitor)
            string host = "tryphon.gsg.capital";
            int port = 11000;

            bool socketStatus = NetworkUtils.IsProcessListening(host, port);

            Assert.IsTrue(socketStatus);

            // 2. Test something that does not exist
            host = "gsg-prod-1.gsg.capital";
            port = 12000;

            socketStatus = NetworkUtils.IsProcessListening(host, port);

            Assert.IsFalse(socketStatus);
        }
    }
}
