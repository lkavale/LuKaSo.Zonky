using System;

namespace LuKaSo.Zonky.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class SpreadsheetSheetAttribute : Attribute
    {
        /// <summary>
        /// Sheet name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Spreadsheet name
        /// </summary>
        /// <param name="name"></param>
        public SpreadsheetSheetAttribute(string name)
        {
            Name = name;
        }
    }
}
