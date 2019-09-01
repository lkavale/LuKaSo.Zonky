using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using LuKaSo.Zonky.Extesions;

namespace LuKaSo.Zonky.Tests.Common
{
    [TestClass]
    public class HttpRequestMessageTests
    {
        private ZonkyHttpRequestMessage _request;
        private readonly Uri _address = new Uri("http://www.zonky.cz/test");

        [TestInitialize]
        public void Init()
        {
            _request = new ZonkyHttpRequestMessage(HttpMethod.Get, _address);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreEqual(HttpMethod.Get, _request.Method);
            Assert.AreEqual(_address, _request.RequestUri);
            Assert.AreEqual("application/json", _request.Headers.GetValues("Accept").First());
        }

        [TestMethod]
        public void AddRequestPaging()
        {
            var page = 10;
            var pageSize = 100;

            _request.AddRequestPaging(page, pageSize);

            Assert.AreEqual(page, int.Parse(_request.Headers.GetValues("x-page").First()));
            Assert.AreEqual(pageSize, int.Parse(_request.Headers.GetValues("x-size").First()));
        }

        [TestMethod]
        public void AddRequestAuthorization()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            _request.AddRequestAuthorization(token);

            Assert.AreEqual($"Bearer {token.AccessToken.ToString()}", _request.Headers.GetValues("Authorization").First());
        }

        [TestMethod]
        public void AddRequestFilter()
        {
            var filter = new FilterOptions();
            filter.Add("x", "10");

            _request.AddRequestFilter(filter);

            Assert.AreEqual(_address.AppendFilterOptions(filter), _request.RequestUri);
        }
    }
}
