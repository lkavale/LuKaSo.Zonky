using LuKaSo.Zonky.Attributes;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LuKaSo.Zonky.Common
{
    public class SpreadsheetProcessor<T> where T : new()
    {
        /// <summary>
        /// Process workbook
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public IEnumerable<T> ProcessWorkbook(XSSFWorkbook workbook)
        {
            int yIndexMin = 0;

            var sheet = GetSheet(workbook);
            var verticalCoordinateAttribute = (SpreadsheetVerticalCoordinateAttribute)typeof(T)
                .GetCustomAttributes(typeof(SpreadsheetVerticalCoordinateAttribute), false)
                .FirstOrDefault();

            if (verticalCoordinateAttribute != null)
            {
                yIndexMin = verticalCoordinateAttribute.ValueIndex;
            }

            for (var y = yIndexMin; y <= sheet.LastRowNum; y++)
            {
                var row = sheet.GetRow(y);
                var item = new T();
                var properties = typeof(T)
                    .GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(SpreadsheetHorizontalCoordinateAttribute), false).Any());

                foreach (var property in properties)
                {
                    var horizontalCoord = (SpreadsheetHorizontalCoordinateAttribute)property
                        .GetCustomAttributes(typeof(SpreadsheetHorizontalCoordinateAttribute), false)
                        .Single();

                    var cell = row.GetCell(horizontalCoord.ValueIndex);

                    try
                    {
                        var data = Convert(cell, property.PropertyType);
                        property.SetValue(item, data);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Cell {horizontalCoord.Value} on line {y} cannot be converted", ex);
                    }
                }

                yield return item;
            }
        }

        /// <summary>
        /// Get sheet from spreadsheet workbook 
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private ISheet GetSheet(XSSFWorkbook workbook)
        {
            var sheetName = string.Empty;
            var sheetAttribute = (SpreadsheetSheetAttribute)typeof(T)
                .GetCustomAttributes(typeof(SpreadsheetSheetAttribute), false)
                .FirstOrDefault();

            if (sheetAttribute != null)
            {
                sheetName = sheetAttribute.Name;
            }

            return workbook.GetSheet(sheetName);
        }

        /// <summary>
        /// Convert ICell value to desired type
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Convert(ICell cell, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (cell == null)
            {
                return null;
            }

            switch (cell.CellType)
            {
                case CellType.String:
                    return CovertValue(cell.StringCellValue, type);
                case CellType.Numeric:
                    return CovertValue(cell.NumericCellValue, type);
                case CellType.Boolean:
                    return CovertValue(cell.BooleanCellValue, type);
                case CellType.Blank:
                    return Activator.CreateInstance(type);
                default:
                    throw new NotSupportedException($"Not supported coversion {cell.CellType.ToString()}.");
            }
        }

        /// <summary>
        /// Convert string value to desired type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CovertValue(string value, Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }
                return CovertValue(value, Nullable.GetUnderlyingType(type));
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return value;
                case TypeCode.DateTime:
                    return DateTime.Parse(value, CultureInfo.InvariantCulture);
                case TypeCode.Boolean:
                    return value.ToLower() == "ano";
                case TypeCode.Int16:
                    return short.Parse(value);
                case TypeCode.Int32:
                    return int.Parse(value);
                case TypeCode.Int64:
                    return long.Parse(value);
                case TypeCode.Double:
                    return double.Parse(value);
                default:
                    throw new NotSupportedException($"Not supported coversion from string value to {type.ToString()}.");
            }
        }

        /// <summary>
        /// Convert double value to desired type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CovertValue(double value, Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return CovertValue(value, Nullable.GetUnderlyingType(type));
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int16:
                    return (short)value;
                case TypeCode.Int32:
                    return (int)value;
                case TypeCode.Int64:
                    return (long)value;
                case TypeCode.Decimal:
                    return (decimal)value;
                case TypeCode.Double:
                    return value;
                case TypeCode.String:
                    return value.ToString(CultureInfo.InvariantCulture);
                default:
                    throw new NotSupportedException($"Not supported coversion from numeric value to {type.Name}.");
            }
        }

        /// <summary>
        /// Convert bool value to desired type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CovertValue(bool value, Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return CovertValue(value, Nullable.GetUnderlyingType(type));
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                    return value ? 1 : 0;
                case TypeCode.String:
                    return value ? "1" : "0";
                default:
                    throw new NotSupportedException($"Not supported coversion from bool value to {type.Name}.");
            }
        }
    }
}
