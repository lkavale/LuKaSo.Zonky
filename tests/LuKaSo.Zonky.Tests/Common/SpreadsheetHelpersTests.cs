using LuKaSo.Zonky.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LuKaSo.Zonky.Tests.Common
{
    [TestClass]
    public class SpreadsheetHelpersTests
    {
        [TestMethod]
        public void GetVerticalCoordinateIndexPossitive()
        {
            var val = SpreadsheetHelpers.GetVerticalCoordinateIndex(1);
            Assert.AreEqual(0, val);

            val = SpreadsheetHelpers.GetVerticalCoordinateIndex(2);
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void GetVerticalCoordinateIndexZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SpreadsheetHelpers.GetVerticalCoordinateIndex(0));
        }

        [TestMethod]
        public void GetVerticalCoordinateIndexNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SpreadsheetHelpers.GetVerticalCoordinateIndex(-1));
        }

        [TestMethod]
        public void GetHorizontalCoordinateIndexA()
        {
            // A = 1
            var val = SpreadsheetHelpers.GetHorizontalCoordinateIndex("A");
            Assert.AreEqual(0, val);
        }

        [TestMethod]
        public void GetHorizontalCoordinateIndexB()
        {
            // B = 2
            var val = SpreadsheetHelpers.GetHorizontalCoordinateIndex("B");
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void GetHorizontalCoordinateIndexAA()
        {
            // Z = 26, AA = 26 + 1
            var val = SpreadsheetHelpers.GetHorizontalCoordinateIndex("AA");
            Assert.AreEqual(26, val);
        }

        [TestMethod]
        public void GetHorizontalCoordinateIndexLowerCase()
        {
            var val = SpreadsheetHelpers.GetHorizontalCoordinateIndex("a");
            Assert.AreEqual(0, val);

            val = SpreadsheetHelpers.GetHorizontalCoordinateIndex("b");
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void GetHorizontalCoordinateIndexInvalidChar()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SpreadsheetHelpers.GetHorizontalCoordinateIndex("-"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SpreadsheetHelpers.GetHorizontalCoordinateIndex(""));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SpreadsheetHelpers.GetHorizontalCoordinateIndex("ř"));
        }
    }
}
