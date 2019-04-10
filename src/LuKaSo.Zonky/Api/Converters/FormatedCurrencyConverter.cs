using Newtonsoft.Json;
using System;

namespace LuKaSo.Zonky.Api.Converters
{
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
            stringValue = stringValue.Replace(" ", string.Empty);

            if (decimal.TryParse(stringValue, out var decimalValue))
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
