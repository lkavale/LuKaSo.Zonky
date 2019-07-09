using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Tests.Common.Client
{
    [TestClass]
    public class ZonkyClientTests
    {
        private ZonkyClientWrapper _zonkyClientWrapped;

        [TestInitialize]
        public void Init()
        {
            _zonkyClientWrapped = new ZonkyClientWrapper();
        }

        [TestMethod]
        public void SplitRequestAsyncOneNotFullPage()
        {
            var amount = 10;
            async Task<IEnumerable<int>> func(int page, int pageSize) => await Task.Run(() => Enumerable.Range(0, pageSize - 1));

            var data = _zonkyClientWrapped.GetDataSplitRequestWrappedAsync(amount, func, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(amount - 1, data.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, amount - 1), data));
        }

        [TestMethod]
        public void SplitRequestAsyncOneFullPage()
        {
            var amount = 10;
            async Task<IEnumerable<int>> func(int page, int pageSize) => await Task.Run(() =>
            {
                if (page == 0)
                {
                    return Enumerable.Range(0, pageSize);
                }

                return Enumerable.Empty<int>();
            });

            var data = _zonkyClientWrapped.GetDataSplitRequestWrappedAsync(amount, func, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(amount, data.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, amount), data));
        }

        [TestMethod]
        public void SplitRequestAsyncOneFullPageAndOneItem()
        {
            var amount = 10;
            async Task<IEnumerable<int>> func(int page, int pageSize) => await Task.Run(() =>
            {
                if (page == 0)
                {
                    return Enumerable.Range(0, pageSize);
                }

                return Enumerable.Range(pageSize, 1);
            });

            var data = _zonkyClientWrapped.GetDataSplitRequestWrappedAsync(amount, func, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(amount + 1, data.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, amount + 1), data));
        }

        [TestMethod]
        public void SplitRequestAsyncTwoFullPages()
        {
            var amount = 10;
            async Task<IEnumerable<int>> func(int page, int pageSize) => await Task.Run(() =>
            {
                if (page < 2)
                {
                    return Enumerable.Range(page * pageSize, pageSize);
                }

                return Enumerable.Empty<int>();
            });

            var data = _zonkyClientWrapped.GetDataSplitRequestWrappedAsync(amount, func, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(amount * 2, data.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, 2 * amount), data));
        }

        [TestMethod]
        public void SplitRequestAsyncTwoFullPagesAndOneItem()
        {
            var amount = 10;
            async Task<IEnumerable<int>> func(int page, int pageSize) => await Task.Run(() =>
            {
                if (page < 2)
                {
                    return Enumerable.Range(page * pageSize, pageSize);
                }

                return Enumerable.Range(page * pageSize, 1);
            });

            var data = _zonkyClientWrapped.GetDataSplitRequestWrappedAsync(amount, func, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual((amount * 2) + 1, data.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0, (2 * amount) + 1), data));
        }
    }
}
