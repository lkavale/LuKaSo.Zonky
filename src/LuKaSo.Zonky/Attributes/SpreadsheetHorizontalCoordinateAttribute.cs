using LuKaSo.Zonky.Common;
using System;

namespace LuKaSo.Zonky.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    internal class SpreadsheetHorizontalCoordinateAttribute : Attribute
    {
        /// <summary>
        /// Horizontal coord
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Horizontal coord index
        /// </summary>
        public int ValueIndex { get; }

        /// <summary>
        /// Spreadsheet horizontal cord
        /// </summary>
        /// <param name="value"></param>
        public SpreadsheetHorizontalCoordinateAttribute(string value)
        {
            Value = value;
            ValueIndex = SpreadsheetHelpers.GetHorizontalCoordinateIndex(Value);
        }
    }
}
