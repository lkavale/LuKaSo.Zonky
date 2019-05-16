using LuKaSo.Zonky.Common;
using System;

namespace LuKaSo.Zonky.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class SpreadsheetHorizontalCoordinatesRangeAttribute : Attribute
    {
        /// <summary>
        /// Minimum horizontal coord
        /// </summary>
        public string Min { get; }

        /// <summary>
        /// Maximum horizontal coord
        /// </summary>
        public string Max { get; }

        /// <summary>
        /// Minimum horizontal coord index
        /// </summary>
        public int MinIndex { get; }

        /// <summary>
        /// Maximum horizontal coord index
        /// </summary>
        public int MaxIndex { get; }

        /// <summary>
        /// Spreadsheet horizontal coord range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SpreadsheetHorizontalCoordinatesRangeAttribute(string min, string max)
        {
            Min = min;
            Max = max;
            MinIndex = SpreadsheetHelpers.GetHorizontalCoordinateIndex(Min);
            MaxIndex = SpreadsheetHelpers.GetHorizontalCoordinateIndex(Max);
        }
    }
}
