using LuKaSo.Zonky.Common;
using System;

namespace LuKaSo.Zonky.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class SpreadsheetVerticalCoordinatesRangeAttribute : Attribute
    {
        /// <summary>
        /// Minimum vertial coord
        /// </summary>
        public int Min { get; }

        /// <summary>
        /// Maximum vertical coord
        /// </summary>
        public int Max { get; }

        /// <summary>
        /// Minimum vertical coord index
        /// </summary>
        public int MinIndex { get; }

        /// <summary>
        /// Maximum vertical coord index
        /// </summary>
        public int MaxIndex { get; }

        /// <summary>
        /// Spreadsheet vertical coord range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SpreadsheetVerticalCoordinatesRangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
            MinIndex = SpreadsheetHelpers.GetVerticalCoordinateIndex(Min);
            MaxIndex = SpreadsheetHelpers.GetVerticalCoordinateIndex(Max);
        }
    }
}
