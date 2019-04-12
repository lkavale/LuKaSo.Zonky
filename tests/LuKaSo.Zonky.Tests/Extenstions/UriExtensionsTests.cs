using LuKaSo.Zonky.Api.Extesions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuKaSo.Zonky.Tests.Extenstions
{
    [TestClass]
    public class UriExtensionsTests
    {
        private Uri _baseUrl;

        [TestInitialize]
        public void Init()
        {
            _baseUrl = new Uri("https://www.test.cz/data");
        }

        [TestMethod]
        public void AttachOneParameter()
        {
            var vars = new Dictionary<string, string>() { { "test", "1" } };
            var uri = _baseUrl.AttachQueryParameters(vars);

            CheckVariables(uri, vars);
        }

        [TestMethod]
        public void AttachTwoParameters()
        {
            var vars = new Dictionary<string, string>() { { "test", "1" }, { "tset", "abcd" } };
            var uri = _baseUrl.AttachQueryParameters(vars);

            CheckVariables(uri, vars);
        }

        [TestMethod]
        public void AttachTwiceOneParameters()
        {
            var vars1 = new Dictionary<string, string>() { { "test", "1" } };
            var vars2 = new Dictionary<string, string>() { { "tset", "abcd" } };

            var uri = _baseUrl.AttachQueryParameters(vars1).AttachQueryParameters(vars2);

            CheckVariables(uri, vars1.Concat(vars2).ToDictionary(d => d.Key, d => d.Value));
        }

        [TestMethod]
        public void AttachTwiceTwoParameters()
        {
            var vars1 = new Dictionary<string, string>() { { "test", "1" }, { "tset", "abcd" } };
            var vars2 = new Dictionary<string, string>() { { "fghjf", "135" }, { "sfvsfd", "sfsv" } };

            var uri = _baseUrl.AttachQueryParameters(vars1).AttachQueryParameters(vars2);

            CheckVariables(uri, vars1.Concat(vars2).ToDictionary(d => d.Key, d => d.Value));
        }

        [TestMethod]
        public void AttachOverwriteParameters()
        {
            var vars1 = new Dictionary<string, string>() { { "test", "1" } };
            var vars2 = new Dictionary<string, string>() { { "test", "2" } };

            var uri = _baseUrl.AttachQueryParameters(vars1).AttachQueryParameters(vars2);

            CheckVariables(uri, vars2);
        }

        private void CheckVariables(Uri uri, IDictionary<string, string> data)
        {
            var queryVars = HttpUtility.ParseQueryString(uri.Query);

            Assert.AreEqual(data.Count(), queryVars.AllKeys.Count());

            foreach (var kpv in data)
            {
                Assert.IsTrue(queryVars.AllKeys.Contains(kpv.Key));
                Assert.IsTrue(string.Join("", queryVars.GetValues(kpv.Key)) == kpv.Value);
            }
        }
    }
}
