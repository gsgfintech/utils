using Capital.GSG.FX.Utils.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capital.GSG.FX.Utils.Tests
{
    [TestClass]
    public class JsonUtilsTests
    {
        [TestMethod]
        public void TestIsValidJson()
        {
            string valid = @"{ 'Name': 'SomeName' }";

            var result = JsonUtils.IsValidJson(valid);
            Assert.IsTrue(result.IsValid, result.ValidationMessage);

            string invalid = "BadJson";

            result = JsonUtils.IsValidJson(invalid);
            Assert.IsFalse(result.IsValid);

            invalid = "{BadJson}";

            result = JsonUtils.IsValidJson(invalid);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void TestTryParseJson()
        {
            JToken jtoken;

            string valid = @"{ 'Name': 'SomeName' }";

            var result = JsonUtils.TryParseJson(valid, out jtoken);
            Assert.IsTrue(result);
            Assert.IsNotNull(jtoken);

            string invalid = "BadJson";

            result = JsonUtils.TryParseJson(invalid, out jtoken);
            Assert.IsFalse(result);
            Assert.IsNull(jtoken);

            invalid = "{BadJson}";

            result = JsonUtils.TryParseJson(invalid, out jtoken);
            Assert.IsFalse(result);
            Assert.IsNull(jtoken);
        }
    }
}
