using System;
using System.Linq;

namespace LuKaSo.Zonky.Common
{
    internal static class SpreadsheetHelpers
    {
        /// <summary>
        /// Convert alpha column value to index
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetHorizontalCoordinateIndex(string value)
        {
            if (value == string.Empty || value.Any(ch => !char.IsLetter(ch)) || value.Any(ch => ch > 127))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            var numValue = value
                .Select(ch => char.ToUpper(ch) - 64)
                .Aggregate(0, (a, b) => (a * 26) + b);

            return numValue < 1 ? 0 : numValue - 1;
        }

        /// <summary>
        /// Convert numeric row value to index
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetVerticalCoordinateIndex(int value)
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return value - 1;
        }
    }
}
