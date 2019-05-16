using LuKaSo.Zonky.Common;
using System;

namespace LuKaSo.Zonky.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    internal class SpreadsheetVerticalCoordinateAttribute : Attribute
    {
        /// <summary>
        /// Vertical coord
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Vertical coord index
        /// </summary>
        public int ValueIndex { get; }

        /// <summary>
        /// Spreadsheet vertical coord
        /// </summary>
        /// <param name="value"></param>
        public SpreadsheetVerticalCoordinateAttribute(int value)
        {
            Value = value;
            ValueIndex = SpreadsheetHelpers.GetVerticalCoordinateIndex(Value);
        }
    }
}
