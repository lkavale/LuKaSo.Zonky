using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LuKaSo.Zonky.Tests.Unit
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
        public void AddSize()
        {
            var size = 100;

            _request.AddSize(size);

            Assert.AreEqual(size, int.Parse(_request.Headers.GetValues("x-size").First()));
        }

        [TestMethod]
        public void AddPaging()
        {
            var page = 10;
            var pageSize = 100;

            _request.AddPaging(page, pageSize);

            Assert.AreEqual(page, int.Parse(_request.Headers.GetValues("x-page").First()));
            Assert.AreEqual(pageSize, int.Parse(_request.Headers.GetValues("x-size").First()));
        }

        [TestMethod]
        public void AddBearerAuthorization()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            _request.AddBearerAuthorization(token);

            Assert.AreEqual($"Bearer {token.AccessToken.ToString()}", _request.Headers.GetValues("Authorization").First());
        }

        [TestMethod]
        public void AddBasicAuthorization()
        {
            var token = Guid.NewGuid().ToString();

            _request.AddBasicAuthorization(token);

            Assert.AreEqual($"Basic {token}", _request.Headers.GetValues("Authorization").First());
        }

        [TestMethod]
        public void AddFilterOptions()
        {
            var filter = new FilterOptions();
            filter.Add("x", "10");

            _request.AddFilterOptions(filter);

            Assert.AreEqual(_address.AppendFilterOptions(filter), _request.RequestUri);
        }

        [TestMethod]
        public void AddQueryParameters()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "x", "10" },
                { "y", "20" }
            };

            _request.AddQueryParameters(parameters);

            Assert.AreEqual(_address.AttachQueryParameters(parameters), _request.RequestUri);
        }

        [TestMethod]
        public void AddParametersMultipletimes()
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

            _request.AddQueryParameters(parameters1);
            _request.AddQueryParameters(parameters2);

            var allParameters = parameters1.Concat(parameters2).ToDictionary(x => x.Key, x => x.Value);

            Assert.AreEqual(_address.AttachQueryParameters(allParameters), _request.RequestUri);
        }

        [TestMethod]
        public void AddParametersRewrite()
        {
            var parameters1 = new Dictionary<string, string>()
            {
                { "x", "10" }
            };

            var parameters2 = new Dictionary<string, string>()
            {
                { "x", "30" }
            };

            _request.AddQueryParameters(parameters1);
            _request.AddQueryParameters(parameters2);

            Assert.AreEqual(_address.AttachQueryParameters(parameters2), _request.RequestUri);
        }

        [TestMethod]
        public void AddJsonContent()
        {
            var @object = new { p1 = 10, p2 = "Test" };
            var settings = new JsonSerializerSettings();

            _request.AddJsonContent(@object, settings);

            Assert.AreEqual(JsonConvert.SerializeObject(@object, settings), _request.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
    }
}
