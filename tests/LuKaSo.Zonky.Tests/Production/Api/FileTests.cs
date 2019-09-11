using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Models.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace LuKaSo.Zonky.Tests.Production.Api
{
    [TestClass]
    public class FileTests
    {
        private ZonkyApi _zonkyApi;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
        }

        [TestMethod]
        public void GetLoanbookUrlOkGetLoanbookFileAddressOk()
        {
            var file = _zonkyApi.GetLoanbookFileAddressAsync().GetAwaiter().GetResult();
            // Loanbook document address before zonky changes https://zonky.cz/page-assets/documents/loanbook[0-1][0-9]_20[1-2][0-9].xlsx
            // Loanbook document address before zonky changes https://zonky.cz/page-assets/images/risk/loanbook/archiv_pujcek_[0-3][0-9]_[0-1][0-9]_20[1-2][0-9].xlsx
            // Loanbook document address before zonky changes https://zonky.cz/page-assets/documents/archiv_pujcek_[0-3][0-9]_[0-1][0-9]_20[1-2][0-9].xlsx
            var regex = new Regex("https://zonky.cz/page-assets/images/risk/loanbook/archiv_pujcek_[0-3][0-9]_[0-1][0-9]_20[1-2][0-9].xlsx");

            Assert.IsTrue(regex.IsMatch(file.ToString()));
        }

        [TestMethod]
        [TestCategory("Full")]
        public void GetLoanbookOk()
        {
            var file = _zonkyApi.GetLoanbookFileAddressAsync().GetAwaiter().GetResult();
            var processor = new SpreadsheetProcessor<LoanbookItem>();
            var data = _zonkyApi.GetLoanbookAsync(file, processor).GetAwaiter().GetResult();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }
    }
}
