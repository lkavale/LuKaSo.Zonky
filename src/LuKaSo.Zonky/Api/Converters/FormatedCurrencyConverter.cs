using Newtonsoft.Json;
using System;
using System.Globalization;

namespace LuKaSo.Zonky.Api.Converters
{
    /// <summary>
    /// JSON converter parsing formated numbers to decimal
    /// For example string 7 256,75 to decimal 7256.75
    /// </summary>
    public class FormatedCurrencyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var stringValue = reader.Value.ToString();
            stringValue = stringValue.Replace(" ", string.Empty).Replace(',', '.');

            if (decimal.TryParse(stringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out var decimalValue))
            {
                return decimalValue;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
