using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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

            _request.AddRequestBearerAuthorization(token);

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

        [TestMethod]
        public void AddRequestParameters()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "x", "10" },
                { "y", "20" }
            };

            _request.AddRequestParameters(parameters);

            Assert.AreEqual(_address.AttachQueryParameters(parameters), _request.RequestUri);
        }

        [TestMethod]
        public void AddRequestParametersMultipletimes()
        {
            var parameters1 = new Dictionary<string, string>()
            {
                { "x", "10" },
                { "y", "20" }
            };

            var parameters2 = new Dictionary<string, string>()
            {
                { "z", "30" }
            };

            _request.AddRequestParameters(parameters1);
            _request.AddRequestParameters(parameters2);

            var allParameters = parameters1.Concat(parameters2).ToDictionary(x => x.Key, x => x.Value);

            Assert.AreEqual(_address.AttachQueryParameters(allParameters), _request.RequestUri);
        }

        [TestMethod]
        public void AddRequestParametersRewrite()
        {
            var parameters1 = new Dictionary<string, string>()
            {
                { "x", "10" }
            };

            var parameters2 = new Dictionary<string, string>()
            {
                { "x", "30" }
            };

            _request.AddRequestParameters(parameters1);
            _request.AddRequestParameters(parameters2);

            Assert.AreEqual(_address.AttachQueryParameters(parameters2), _request.RequestUri);
        }
    }
}
